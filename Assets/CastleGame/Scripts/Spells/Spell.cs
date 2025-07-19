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

    // states
    protected bool VisualInitialized = false;
    private float _timeOfInitialization = 0.0f;
    /*
     * Before VisualInit():
     * - VisualInitialized=false
     * 
     * After VisualInit():
     * - VisualInitialized=true
     * 
     * After OnVisualEnd() / VisualReset():
     * - resets to "Before VisualInit()"
     */

    public Player_CG Owner { get; set; }

    public VisualEffect VisualEffect
    {
        get { return _effect; }
    }


    public virtual void Init(Player_CG owner)
    {
        this.Owner = owner;
    }

    public virtual void Awake()
    {
        _effect = GetComponentInChildren<VisualEffect>();
        if( _effect == null )
        {
            Debug.LogError("Visual component was not attached to Spell's gameObject");
        }
        VisualReset();
    }

    public virtual void Start()
    {

    }

    public virtual void FixedUpdate()
    {
        if(VFX == null && VisualInitialized)
        {
            OnVisualEnd();
        }
    }

    public virtual void Update()
    {
        
    }




    public void VisualInit()
    {
        VisualReset();
        VFX = this.VisualEffect.gameObject.AddComponent<PlayDecalVFX.PlayVFX>();
        VisualInitialized = true;
        _timeOfInitialization = Time.time;
    }

    public virtual void OnVisualEnd()
    {
        if(VisualInitialized)
        {
            VisualReset();
        }
    }

    public virtual void VisualReset()
    {
        if (VFX != null)
        {
            _effect.pause = false;
            _effect.Reinit();
            Destroy(VFX);
            VFX = null;
            VisualInitialized = false;
        }
    }

    public virtual void VisualPause()
    {
        if(VFX != null)
        {
            VFX.Pause();
        }
    }

    // Partial Visual Effect
    public void VisualPartial(float pauseDelay = 0.0f)
    {
        VisualInit();
        StartCoroutine(PauseAfterDelay_CR(pauseDelay));

        IEnumerator PauseAfterDelay_CR(float pauseDelay = 0.0f)
        {
            // ensure the visual initialization has not changed during the delay
            yield return new WaitForSeconds(pauseDelay);
            float currentTime = Time.time;
            if (currentTime >= _timeOfInitialization)
            {
                VisualPause();
            }
        }
    }

    // One Complete Visual Effect
    public void VisualSingle()
    {
        VisualInit();
    }
}
