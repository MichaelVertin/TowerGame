using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Warrior : MonoBehaviour, IOwnable
{
    public Player Owner { get; set; }
    private bool _InitSuccess;

    private Animator _animator;

    protected WarriorBody _body;
    [SerializeField] protected WarriorRange RangeOfVision;
    [SerializeField] protected WarriorRange RangeOfEffect;

    [SerializeField] private RuntimeAnimatorController _attackController;
    [SerializeField] private RuntimeAnimatorController _moveController;
    [SerializeField] private RuntimeAnimatorController _idleController;
    [SerializeField] private RuntimeAnimatorController _deathController;

    [SerializeField] public int Cost;
    [SerializeField] protected float _damage;
    [SerializeField] public float speed = 0f;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;

        _body = GetComponentInChildren<WarriorBody>();
    }

    public void Init(Player owner)
    {
        this.Owner = owner;

        _InitSuccess = true;
    }

    protected void Start()
    {
        if (!_InitSuccess)
        {
            Destroy(this.gameObject);
            Debug.LogError("Warrior.Init not called before Warrior.Start");
        }
        OnAvailable();
    }

    protected virtual void FixedUpdate()
    {
        if( speed != 0.0f && _animator.runtimeAnimatorController == _moveController )
        {
            this.transform.position += Owner.direction * speed;
        }
    }

    #region DamagableClass
    [SerializeField] protected float _maxHealth;
    private float _currentHealth = 0f;
    public float Health
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            if (Health <= .00001f)
            {
                OnDeath();
            }
        }
    }
    #endregion

    public void OnAvailable()
    {
        // this Warrior has died
        if (Health <= 0 || _animator.runtimeAnimatorController == _deathController)
        {
            // skip if no health
        }
        // Enemy in range -> attack enemy
        else if( GetActiveAnimation(out RuntimeAnimatorController attackCTRL))
        {
            _animator.runtimeAnimatorController = _moveController;
            _animator.runtimeAnimatorController = attackCTRL;
        }
        // not dead and no enemies -> move forward
        else
        {
            _animator.runtimeAnimatorController = _moveController;
            StartCoroutine(CallOnAvailable(0.0f));
        }
    }

    private IEnumerator CallOnAvailable( float time )
    {
        yield return new WaitForSeconds( time );
        OnAvailable();
    }

    #region Death
    // Called on death of warrior in Damagable class
    public void OnDeath()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        _animator.runtimeAnimatorController = _deathController;
        Destroy(this.gameObject);
        yield return new WaitForSeconds(0f);
    }
    #endregion Death

    public void AttackEnemy(Warrior warrior)
    {
        warrior.Health -= _damage;
    }

    public void AttackBase(Base otherBase)
    {
        otherBase.Health -= _damage;
    }


    public virtual void OnAttack()
    {
        if( RangeOfEffect.GetEnemyWarrior(this, out Warrior enemyWarrior) )
        {
            AttackEnemy(enemyWarrior);
        }

        else if (RangeOfEffect.GetEnemyBase(this, out Base enemyBase))
        {
            AttackBase(enemyBase);
        }
    }

    public virtual void OnAttackEnd()
    {
        OnAvailable();
    }

    public virtual bool GetActiveAnimation(out RuntimeAnimatorController EffectCTRL)
    {
        if( RangeOfVision.GetEnemyWarrior(this, out _ ) || RangeOfVision.GetEnemyBase(this, out _ ) )
        {
            EffectCTRL = _attackController;
            return true;
        }
        EffectCTRL = null;
        return false;
    }
}
