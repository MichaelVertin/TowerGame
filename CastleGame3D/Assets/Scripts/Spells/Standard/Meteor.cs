using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : SpellStandard
{
    [SerializeField] protected int Damage;
    protected override void OnApplicationStart()
    {
        foreach( Warrior warrior in RangeOfEffect.GetEnemyWarriors(this) )
        {
            warrior.Health -= Damage;
        }
    }

    protected override void OnApplicationEnd()
    {
        Destroy(this.gameObject);
    }
}
