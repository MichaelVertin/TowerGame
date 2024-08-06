using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPartial : Spell
{
    [SerializeField] float duration = 1.25f;

    public override void Awake()
    {
        base.Awake();
        VisualInit();
        Invoke("_VisualEffectPause", duration);
    }

    protected void _VisualEffectPause()
    {
        this.VisualEffect.pause = true;
    }

    public override void OnVisualEnd()
    {
        base.OnVisualEnd();
        
    }
}
