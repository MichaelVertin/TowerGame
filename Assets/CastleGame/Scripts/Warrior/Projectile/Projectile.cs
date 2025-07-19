using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour, IOwnable
{
    private bool isAirborne = false;
    private Warrior __owner;
    private Vector3 __dir;
    private Range Warrior_RangeOfEffect;
    [SerializeField] protected Range Projectile_RangeOfEffect;
    [SerializeField] protected Transform handReference;
    [SerializeField] protected Transform groundReference;
    [SerializeField] protected Transform poleTransform;

    public Player_CG Owner { get; set; }

    public void Awake()
    {
        ProjectileExit projectileExit = GetComponentInChildren<ProjectileExit>();
        projectileExit.Init(this);
    }

    public void Init(Range RangeOfEffect)
    {
        this.Warrior_RangeOfEffect = RangeOfEffect;
    }

    public void launch(Warrior owner, Vector3 direction)
    {
        if (!isAirborne)
        {
            // detach from parent
            transform.parent = null;
            
            __owner = owner;
            Owner = __owner.Owner;
            __dir = Owner.direction;
            isAirborne = true;

            poleTransform.parent = groundReference;

            SetToGround();
        }
    }

    public void SetToGround()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
        Vector3 basePosition = this.transform.localPosition;
        Vector3 baseRotation = Vector3.zero;
        baseRotation.x = 0;
        baseRotation.y = 90;
        baseRotation.z = 0;
        basePosition.y = 0;
        this.transform.localPosition = basePosition;
        this.transform.localEulerAngles = baseRotation;

        Projectile_RangeOfEffect.transform.localPosition = Vector3.zero;
        Projectile_RangeOfEffect.transform.localEulerAngles = Vector3.zero;

        poleTransform.localPosition = Vector3.zero;
        poleTransform.localRotation = Quaternion.identity;
        poleTransform.localScale *= 3;
    }

    public void FixedUpdate()
    {
        if( isAirborne )
        {
            transform.position += __dir;
        }
    }

    
    public void Update()
    {
        if( isAirborne )
        {
            if ( Projectile_RangeOfEffect.GetEnemyWarrior(__owner, out Warrior warriorTest ) )
            {
                __owner.AttackEnemy(warriorTest);
                Destroy(this.gameObject);
            }
            else if (Projectile_RangeOfEffect.GetEnemyBase(__owner, out Base enemyBaseTest))
            {
                __owner.AttackBase(enemyBaseTest);
                Destroy(this.gameObject);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (__owner == null)
        {
            return;
        }

        if ( other.gameObject == Warrior_RangeOfEffect.gameObject )
        {
            Destroy(this.gameObject);
        }
    }

    public void CallDestroy()
    {
        Destroy(this.gameObject);
    }
}
