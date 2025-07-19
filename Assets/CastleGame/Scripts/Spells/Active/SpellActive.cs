using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpellActive : Spell
{
    [SerializeField] protected Range RangeOfEffect;

    public override void Awake()
    {
        base.Awake();
        if( RangeOfEffect == null )
        {
            Debug.LogError("RangeOfEffect for SpellActive was not assigned");
        }
    }

    public override void Start()
    {
        base.Start();
        VisualSingle();
    }
}
