using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : SpellStandard
{
    [SerializeField] protected float speedMultiplier;

    protected override void OnApplicationStart()
    {
        foreach (Warrior warrior in RangeOfEffect.GetEnemyWarriors(this))
        {
            _affectedWarriors.Add(warrior);
            Animator anim = warrior.GetComponent<Animator>();
            anim.speed *= speedMultiplier;
            warrior.speed *= speedMultiplier;
        }
    }

    protected override void OnApplicationEnd()
    {
        foreach (Warrior warrior in _affectedWarriors)
        {
            if (warrior != null)
            {
                Animator anim = warrior.GetComponent<Animator>();
                anim.speed /= speedMultiplier;
                warrior.speed /= speedMultiplier;
            }
        }
        Destroy(this.gameObject);
    }
}

