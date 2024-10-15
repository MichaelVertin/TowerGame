using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFloat : Spell
{
    [SerializeField] private SpellActive spellToSpawn;
    [SerializeField] float duration = 1.25f;
    public override void Start()
    {
        VisualPartial(duration);
        base.Start();

        // must be initialized immidiately
        if (Owner == null)
        {
            Destroy(this.gameObject);
            Debug.LogError("SpellFloat Not Initialized");
        }
    }

    public override void Update()
    {
        /* 
        // Removed due to need to view spells indepdently from their owner
        // Also would need to know orientation of the Player's spawn
        if (InputManager.GetObjectUnderMouse<Path>(out path))
        {
            SpawnManager.instance.UpdateTransformForCursor(this.transform, owner, path);
        }
        */
        // otherwise, if selected on the ground, set to the position of the ground
        if (InputManager.GetObjectUnderMouse<Ground>(out Vector3 position))
        {
            position.y += SpawnManager.instance.PATH_HEIGHT;
            transform.position = position;
        }


        // place the object on button release
        if (InputManager.WasLeftMouseButtonReleased)
        {
            SpawnManager.instance.Spawn(spellToSpawn, SpawnManager.SPAWN_CONTROL.FROM_CURSOR, Owner);
            Destroy(this.gameObject);
        }
    }
} 