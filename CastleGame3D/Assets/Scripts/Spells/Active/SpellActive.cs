using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpellActive : SpellSingle
{
    protected SpellRange _range;

    public override void Awake()
    {
        base.Awake();
        _range = GetComponentInChildren<SpellRange>();
        if( _range == null )
        {
            Debug.LogError("SpellActive does not have a SpellRange component in its children");
        }
    }
}
