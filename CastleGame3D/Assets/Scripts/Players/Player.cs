using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public List<Transform> _spawnTransforms;
    [SerializeField] public Vector3 direction = new Vector3(0,0,1);

    public List<Warrior> Warriors = new List<Warrior>();
    public List<Spell> Spells = new List<Spell>();

    public virtual void Awake()
    {
        direction = direction.normalized;
    }

    protected virtual void Start()
    {
        OnControlStart();
    }

    protected virtual void FixedUpdate()
    {

    }

    public virtual void OnControlStart()
    {
        
    }

    public virtual bool VerifySpawn( Warrior warrior )
    {
        return true;
    }

    public virtual bool VerifySpawn(Spell spell)
    {
        return true;
    }

}
