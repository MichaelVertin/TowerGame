using PlayDecalVFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Meteor : SpellSingle
{
    [SerializeField] public int Damage;
    public void OnTriggerEnter(Collider other)
    {
        WarriorBody warriorBody = other.GetComponent<WarriorBody>();

        if(warriorBody != null && warriorBody.Warrior.owner != this.owner)
        {
            warriorBody.Warrior.Health -= Damage;
        }
    }

    public void Init(Player owner)
    {
        this.owner = owner;
    }
}


