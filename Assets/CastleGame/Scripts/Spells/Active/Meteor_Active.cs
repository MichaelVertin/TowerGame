using PlayDecalVFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Meteor_Active : SpellActive
{
    [SerializeField] public int Damage;
    [SerializeField] protected float timeBeforeDamage;

    public override void Init(Player_CG owner)
    {
        base.Init(owner);
        Invoke("DamageAllEnemiesInRange", timeBeforeDamage);
    }

    public void DamageAllEnemiesInRange()
    {
        foreach( Warrior warrior in RangeOfEffect.GetEnemyWarriors(this) )
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


