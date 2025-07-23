using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WarriorFloat : MonoBehaviour, IOwnable
{
    [SerializeField] private Warrior warriorToSpawn;
    public Player_CG Owner { get; set; }

    protected void Start()
    {
        // must be initialized immidiately
        if (Owner == null)
        {
            Destroy(this.gameObject);
            Debug.LogError("WarriorFloat Not Initialized");
        }

        UpdatePosition();
    }

    protected void Update()
    {
        UpdatePosition();

        // place the object on button release
        if (InputManager.WasLeftMouseButtonReleased)
        {
            bool foundPath = InputManager.GetObjectUnderMouse<WarriorPath>(out WarriorPath path);
            SpawnManager_CG.instance.Spawn(warriorToSpawn, SpawnManager_CG.SPAWN_CONTROL.FROM_BASE, Owner, path);
            Destroy(this.gameObject);
        }
    }

    protected void UpdatePosition()
    {
        // if selected on the path, set to 10% away from the owner's base
        bool foundPath = InputManager.GetObjectUnderMouse<WarriorPath>(out WarriorPath path);
        if (foundPath)
        {
            SpawnManager_CG.instance.UpdateTransform(SpawnManager_CG.SPAWN_CONTROL.FROM_BASE, this.transform, Owner, path, .1f);
        }
        // otherwise, if selected on the ground, set to the position of the ground
        else if (InputManager.GetObjectUnderMouse<Ground>(out Vector3 position))
        {
            transform.position = position;
        }
    }

    public void Init(UserPlayer_CG ownerPar)
    {
        Owner = ownerPar;
    }
}
