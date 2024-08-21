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
    private WarriorRange RangeOfEffect;
    [SerializeField] protected WarriorRange _range;

    public Player Owner { get; set; }

    public void Awake()
    {
        ProjectileExit projectileExit = GetComponentInChildren<ProjectileExit>();
        projectileExit.Init(this);
    }

    public void Init(WarriorRange RangeOfEffect)
    {
        this.RangeOfEffect = RangeOfEffect;
    }

    public void launch(Warrior owner, Vector3 direction)
    {
        if (!isAirborne)
        {
            // detach from parent, provide a Range
            transform.parent = null;
            GameObject collObj = gameObject.GetComponentInChildren<Rigidbody>().gameObject;

            // set other data
            __owner = owner;
            Owner = __owner.Owner;
            __dir = Owner.direction;
            isAirborne = true;

            // set rotation
            Vector3 rot = this.transform.eulerAngles;
            rot.x = 0.0f;
            rot.y = 0.0f;
            rot.z = 90.0f;
            this.transform.eulerAngles = rot;
        }
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
            if( _range.GetEnemyWarrior(__owner, out Warrior warriorTest ) )
            {
                __owner.AttackEnemy(warriorTest);
                Destroy(this.gameObject);
            }
            else if (_range.GetEnemyBase(__owner, out Base enemyBaseTest))
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

        if ( other.gameObject == RangeOfEffect.gameObject )
        {
            Destroy(this.gameObject);
        }
    }

    public void CallDestroy()
    {
        Destroy(this.gameObject);
    }
}
