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
        List<Warrior> warriors = _range.warriors;
        foreach (Warrior warrior in warriors)
        {
            if (warrior != null && Methods.HasEnemy(this, warrior))
            {
                AttackEnemy(warrior);
            }
        }

        foreach (Base otherBase in _range.bases)
        {
            if (Methods.HasEnemy(this, otherBase))
            {
                AttackBase(otherBase);
            }
        }
    }
}
