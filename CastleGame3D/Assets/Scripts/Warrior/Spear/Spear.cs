using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private bool isAirborne = false;
    [SerializeField]  private Warrior __owner;
    private Vector3 __dir;
    [SerializeField] private WeaponRange __range;

    public void Awake()
    {
        SpearExit spearExit = GetComponentInChildren<SpearExit>();
        spearExit.init(this);
        __range = GetComponentInChildren<WeaponRange>();
    }

    public void launchSpear(Warrior owner, Vector3 direction)
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

        // Destroy spear on exit range
        WarriorRange range = other.GetComponent<WarriorRange>();
        if (range != null)
        {
            Warrior warrior = range.transform.root.GetComponent<Warrior>();
            if (warrior == this.__owner)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
