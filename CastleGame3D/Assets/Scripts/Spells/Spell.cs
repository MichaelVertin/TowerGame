using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.VFX;

public class Spell : MonoBehaviour, IOwnable
{
    [SerializeField] public int Cost = 0;

    protected VisualEffect _effect = null;
    protected PlayDecalVFX.PlayVFX VFX = null;
    protected Collider _collider = null;
    protected bool VisualInitialized = false;
    protected bool OnVisualEndCalled = false;

    public Player Owner { get; set; }

    public VisualEffect VisualEffect
    {
        get { return _effect; }
    }

    public virtual void Awake()
    {
        _effect = GetComponentInChildren<VisualEffect>();
        if( _effect == null )
        {
            Debug.LogError("Visual component was not attached to Spell's gameObject");
        }
    }

    public virtual void Start()
    {

    }

    public virtual void FixedUpdate()
    {
        if(VFX == null && VisualInitialized && !OnVisualEndCalled)
        {
            OnVisualEnd();
        }
    }

    public virtual void Update()
    {
        
    }

    public void VisualInit()
    {
        VFX = this.VisualEffect.gameObject.AddComponent<PlayDecalVFX.PlayVFX>();
        VisualInitialized = true;
        OnVisualEndCalled = false;
    }

    public virtual void OnVisualEnd()
    {
        OnVisualEndCalled = true;
    }

    public virtual void Init(Player owner)
    {
        this.Owner = owner;
    }
}
