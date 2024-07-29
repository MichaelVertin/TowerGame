using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WarriorFloat : MonoBehaviour
{
    [SerializeField] private Warrior warriorToSpawn;
    UserPlayer owner;


    protected void Update()
    {
        // must be initialized immidiately
        if( owner == null )
        {
            Destroy(this.gameObject);
            Debug.LogError("WarriorFloat Not Initialized");
        }

        Path path;
        bool foundPath = InputManager.GetObjectUnderMouse<Path>(out path);
        // if selected on the path, set to 10% away from the owner's base
        if (foundPath)
        {
            SpawnManager.instance.UpdateTransformForPath(this.transform, owner, path, .1f);
        }
        // otherwise, if selected on the ground, set to the position of the ground
        else if(InputManager.GetPointUnderMouse<Ground>(out Vector3 position) )
        {
            transform.position = position;
        }


        // place the object on button release
        if (InputManager.WasLeftMouseButtonReleased)
        {
            if( foundPath )
            {
                SpawnManager.instance.SpawnWarriorOnPath(owner, path, warriorToSpawn);
            }
            Destroy(this.gameObject);
        }
    }

    public void Init(UserPlayer ownerPar)
    {
        owner = ownerPar;
    }
}
