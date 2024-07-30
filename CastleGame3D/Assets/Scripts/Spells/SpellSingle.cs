using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSingle : Spell
{
    // immediately initialize visual
    public override void Awake()
    {
        base.Awake();
        VisualInit();
    }

    public override void OnVisualEnd()
    {
        base.OnVisualEnd();
    }
}
