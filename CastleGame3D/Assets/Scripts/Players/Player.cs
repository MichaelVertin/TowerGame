using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public List<Transform> _spawnTransforms;

    [SerializeField] public Warrior _shieldWarrior;
    [SerializeField] public Warrior _swordWarrior;
    [SerializeField] public Warrior _spearWarrior;
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

    public virtual bool VerifyWarriorSpawn( Warrior warrior )
    {
        return true;
    }

    public virtual bool VerifySpellSpawn(Spell spell)
    {
        return true;
    }

}
