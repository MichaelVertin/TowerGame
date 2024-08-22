using PlayDecalVFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Meteor : SpellActive
{
    [SerializeField] public int Damage;
    [SerializeField] protected float timeBeforeDamage;

    public override void Init(Player owner)
    {
        base.Init(owner);
        Invoke("DamageAllEnemiesInRange", timeBeforeDamage);
    }

    public void DamageAllEnemiesInRange()
    {
        foreach( Warrior warrior in _range.GetEnemyWarriors(this) )
        {
            warrior.Health -= Damage;
        }
    }

    public override void OnVisualEnd()
    {
        base.OnVisualEnd();
        Destroy(this.gameObject);
    }
}


