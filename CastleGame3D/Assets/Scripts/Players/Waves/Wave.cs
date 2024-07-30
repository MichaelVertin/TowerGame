using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private AIPlayer owner;

    public void Play()
    {
        foreach( Path path in owner._paths )
        {
            owner.SpawnAfterTime(0f, owner._spearWarrior, path);
        }
    }
}
