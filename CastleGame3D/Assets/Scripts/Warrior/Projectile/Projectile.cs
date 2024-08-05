using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private bool isAirborne = false;
    [SerializeField]  private Warrior __owner;
    private Vector3 __dir;
    [SerializeField] private ProjectileRange __range;

    public void Awake()
    {
        ProjectileExit projectileExit = GetComponentInChildren<ProjectileExit>();
        projectileExit.init(this);
        __range = GetComponentInChildren<ProjectileRange>();
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
            __dir = __owner.owner.direction;
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
            foreach( Warrior warrior in __range.warriors )
            {
                if( warrior.owner != __owner.owner )
                {
                    __owner.AttackEnemy(warrior);
                    Destroy(this.gameObject);
                    return;
                }
            }

            foreach (Base enemyBase in __range.bases)
            {
                if (enemyBase.owner != __owner.owner)
                {
                    __owner.AttackBase( enemyBase );
                    Destroy(this.gameObject);
                    return;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (__owner == null)
        {
            return;
        }

        // Destroy projectile on exit range
        WarriorRange range = other.GetComponent<WarriorRange>();
        if (range != null)
        {
            Warrior warrior = range.transform.root.GetComponent<Warrior>();
            if (warrior == this.__owner)
            {
                Invoke("CallDestroy", .1f);
                //Destroy(this.gameObject);
            }
        }
    }

    public void CallDestroy()
    {
        Destroy(this.gameObject);
    }
}
