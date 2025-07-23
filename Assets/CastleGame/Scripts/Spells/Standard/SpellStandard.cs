using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellStandard : Spell
{
    [SerializeField] protected float applicationDelay = 0.0f;
    [SerializeField] protected float applicationDuration = 0.0f;
    [SerializeField] private float precastDuration = 0.0f;
    [SerializeField] protected Range RangeOfEffect;

    protected bool wasCast = false;
    protected List<Warrior> _affectedWarriors = new();
    private Outline_TG outline = null;

    public override void Start()
    {
        base.Start();
        if (Owner == null)
        {
            Destroy(this.gameObject);
            Debug.LogError("Spell Not Initialized");
        }
        if (!wasCast)
        {
            VisualPartial(precastDuration);
        }
        outline = GetComponentInChildren<Outline_TG>();
    }

    protected abstract void OnApplicationStart();

    protected abstract void OnApplicationEnd();

    public override void Update()
    {
        base.Update();
        UpdatePosition();
    }

    public virtual void UpdatePosition()
    {
        if( wasCast )
        {
            return;
        }
        /* 
        // Removed due to need to view spells indepdently from their owner
        // Also would need to know orientation of the Player's spawn
        if (InputManager.GetObjectUnderMouse<Path>(out path))
        {
            SpawnManager.instance.UpdateTransformForCursor(this.transform, owner, path);
        }
        */
        // otherwise, if selected on the ground, set to the position of the ground
        outline.gameObject.SetActive(false);
        if (InputManager.GetObjectUnderMouse<Ground>(out Vector3 position))
        {
            position.y += SpawnManager_CG.instance.PATH_HEIGHT;
            transform.position = position;
            if(SpawnManager_CG.instance.UpdateTransform(SpawnManager_CG.SPAWN_CONTROL.FROM_CURSOR, transform, Owner))
            {
                outline.gameObject.SetActive(true);
            }
        }

        // place the object on button release
        if (InputManager.WasLeftMouseButtonReleased)
        {
            SpawnManager_CG.instance.Spawn(this, SpawnManager_CG.SPAWN_CONTROL.FROM_CURSOR, Owner);
        }
    }

    // TODO: interact with SpawnManager
    public void Cast()
    {
        if (!wasCast)
        {
            Invoke(nameof(OnApplicationStart), applicationDelay);
            Invoke(nameof(OnApplicationEnd), applicationDelay + applicationDuration);
            VisualSingle();
            wasCast = true;
        }
    }
}
