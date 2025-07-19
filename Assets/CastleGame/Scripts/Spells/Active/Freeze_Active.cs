using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze_Active : SpellActive
{
    [SerializeField] protected float timeBeforeEffect;
    [SerializeField] protected float duration;
    [SerializeField] protected float speedMultiplier;

    protected List<Warrior> _affectedWarriors = new List<Warrior>();

    public override void Init(Player_CG owner)
    {
        base.Init(owner);
        Invoke("Apply", timeBeforeEffect);
        Invoke("UnApply", timeBeforeEffect + duration);
    }

    public void Apply()
    {
        foreach (Warrior warrior in RangeOfEffect.GetEnemyWarriors(this))
        {
            _affectedWarriors.Add(warrior);
            Animator anim = warrior.GetComponent<Animator>();
            anim.speed *= speedMultiplier;
            warrior.speed *= speedMultiplier;
        }
    }

    public void UnApply()
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


