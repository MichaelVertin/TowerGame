using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haste : SpellStandard
{
    [SerializeField] protected float speedMultiplier;

    protected override void OnApplicationStart()
    {
        foreach (Warrior warrior in RangeOfEffect.GetAllyWarriors(this))
        {
            _affectedWarriors.Add(warrior);
            Animator anim = warrior.GetComponent<Animator>();
            anim.speed *= speedMultiplier;
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
            }
        }
        Destroy(this.gameObject);
    }
}

