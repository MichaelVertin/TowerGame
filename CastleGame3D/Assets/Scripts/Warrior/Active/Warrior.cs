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

    [SerializeField] private RuntimeAnimatorController _attackController;
    [SerializeField] private RuntimeAnimatorController _moveController;
    [SerializeField] private RuntimeAnimatorController _idleController;
    [SerializeField] private RuntimeAnimatorController _deathController;

    [SerializeField] public int Cost;
    [SerializeField] protected float _damage;

    private WarriorBody _body;
    protected WarriorRange _range;

    // States:
    /*
     * Moving - moving forward
     * Attacking - attacking enemy or enemy base
     * Dying - displaying death animations
     * 
     * Moving -> Attacking on collision with enemy of enemy base
     * Attacking -> Moving on exit with all enemies and base
     */

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;

        _body = GetComponentInChildren<WarriorBody>();
        _range = GetComponentInChildren<WarriorRange>();
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
        Base enemyBase = null;
        Warrior enemyWarrior = null;
        foreach (Warrior warrior in _range.warriors)
        {
            if (warrior != null && Methods.HasEnemy(this, warrior))
            {
                enemyWarrior = warrior;
                break;
            }
        }

        foreach (Base otherBase in _range.bases)
        {
            if( Methods.HasEnemy(this, otherBase) )
            {
                enemyBase = otherBase;
                break;
            }
        }

        // this Warrior has died
        if (Health <= 0 || _animator.runtimeAnimatorController == _deathController)
        {
            // skip if no health
        }
        // Enemy in range -> attack enemy
        else if (enemyWarrior != null || enemyBase != null)
        {
            _animator.runtimeAnimatorController = _moveController;
            _animator.runtimeAnimatorController = _attackController;
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
        List<Warrior> warriors = _range.warriors;
        Base enemyBase = null;
        Warrior enemyWarrior = null;
        foreach (Warrior warrior in warriors)
        {
            if (warrior != null && Methods.HasEnemy(this,warrior))
            {
                enemyWarrior = warrior;
                break;
            }
        }

        foreach (Base otherBase in _range.bases)
        {
            if (Methods.HasEnemy(this,otherBase))
            {
                enemyBase = otherBase;
                break;
            }
        }

        // Enemy warrior in range -> attack enemy
        if (enemyWarrior != null)
        {
            AttackEnemy(enemyWarrior);
        }
        // Enemy base in range -> attack base
        else if (enemyBase != null)
        {
            AttackBase(enemyBase);
        }
    }

    public virtual void OnAttackEnd()
    {
        OnAvailable();
    }
}
