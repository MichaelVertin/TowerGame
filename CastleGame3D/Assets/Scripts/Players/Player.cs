using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public List<Transform> _spawnTransforms;

    [SerializeField] protected Warrior _warrior1;
    [SerializeField] protected Warrior _warrior2;
    

    public void Start()
    {
        OnControlStart();
    }

    public virtual void OnControlStart()
    {
        
    }

    public virtual bool VerifyWarriorSpawn( Warrior warrior )
    {
        return true;
    }

    public virtual bool VerifySpellSpawn(Spell spell)
    {
        return true;
    }

}
