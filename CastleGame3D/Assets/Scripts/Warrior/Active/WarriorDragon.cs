using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WarriorDragon : Warrior
{
    protected List<ParticleSystem> _particleSystems = new List<ParticleSystem>();

    protected override void Awake()
    {
        base.Awake();
        gameObject.GetComponentsInChildren<ParticleSystem>(_particleSystems);
    }

    public void FlameActivate()
    {
        foreach (ParticleSystem partcielSystem in _particleSystems)
        {
            partcielSystem.Play();
        }
    }

    public void FlameDeactivate()
    {
        foreach (ParticleSystem partcielSystem in _particleSystems)
        {
            partcielSystem.Stop();
        }
    }

    public override void OnAttack()
    {
        foreach (Warrior warrior in RangeOfEffect.GetEnemyWarriors(this) )
        {
            AttackEnemy(warrior);
        }

        if( RangeOfEffect.GetEnemyBase(this, out Base enemyBase ) )
        {
            AttackBase(enemyBase);
        }
    }
}
