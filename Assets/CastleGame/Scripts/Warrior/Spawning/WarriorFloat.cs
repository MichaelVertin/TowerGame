using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WarriorFloat : MonoBehaviour, IOwnable
{
    [SerializeField] private Warrior warriorToSpawn;
    public Player_CG Owner { get; set; }

    protected void Update()
    {
        // must be initialized immidiately
        if( Owner == null )
        {
            Destroy(this.gameObject);
            Debug.LogError("WarriorFloat Not Initialized");
        }

        // if selected on the path, set to 10% away from the owner's base
        bool foundPath = InputManager.GetObjectUnderMouse<Path>(out Path path);
        if (foundPath)
        {
            SpawnManager_CG.instance.UpdateTransform(SpawnManager_CG.SPAWN_CONTROL.FROM_BASE, this.transform, Owner, path, .1f);

        }
        // otherwise, if selected on the ground, set to the position of the ground
        else if(InputManager.GetObjectUnderMouse<Ground>(out Vector3 position) )
        {
            transform.position = position;
        }


        // place the object on button release
        if (InputManager.WasLeftMouseButtonReleased)
        {
            SpawnManager_CG.instance.Spawn(warriorToSpawn, SpawnManager_CG.SPAWN_CONTROL.FROM_BASE, Owner, path);
            Destroy(this.gameObject);
        }
    }

    public void Init(UserPlayer_CG ownerPar)
    {
        Owner = ownerPar;
    }
}
