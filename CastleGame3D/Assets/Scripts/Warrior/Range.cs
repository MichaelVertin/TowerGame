using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;

public class Range : MonoBehaviour
{
    protected List<Warrior> warriors = new List<Warrior>();
    protected List<Base> bases = new List<Base>();

    protected BoxCollider rangeCollider;

    public enum AccessType
    {
        GROUND, 
        FLYING, 
        GROUND_AND_FLYING
    }

    protected virtual void Awake()
    {
        SetCollider();
    }

    #region AccessType
    [SerializeField] protected AccessType accessType = AccessType.GROUND;

    public virtual void SetCollider()
    {
        rangeCollider = GetComponent<BoxCollider>();
        if( rangeCollider == null )
        {
            Debug.LogError("Unable to identify Box Collider for Range");
        }
        Vector3 colliderCenter = rangeCollider.center;
        Vector3 colliderSize = rangeCollider.size;

        // x is always the same
        // y is dependent on AccessType
        // z is defined by user
        colliderCenter.x = 0;
        colliderSize.x = 8;

        if (accessType == AccessType.FLYING)
        {
            colliderCenter.y = 6 + 1 + 3;
            colliderSize.y = 6;
        }
        else if (accessType == AccessType.GROUND_AND_FLYING)
        {
            colliderCenter.y = 6 + 1/2f;
            colliderSize.y = 6 * 2 + 1;
        }
        else if( accessType == AccessType.GROUND)
        {
            colliderCenter.y = 3;
            colliderSize.y = 6;
        }
        else
        {
            Debug.LogError("Unrecognized AccessType");
        }

        rangeCollider.center = colliderCenter;
        rangeCollider.size = colliderSize;
    }
    #endregion

    #region WarriorIdentification
    protected void OnTriggerEnter(Collider collider)
    {
        WarriorBody otherBody = collider.GetComponent<WarriorBody>();

        if (otherBody != null)
        {
            Warrior otherWarrior = otherBody.transform.root.GetComponent<Warrior>();

            Warrior thisWarrior = this.transform.root.GetComponent<Warrior>();

            warriors.Add(otherWarrior);
        }

        Base otherBase = collider.GetComponent<Base>();

        if (otherBase != null)
        {
            bases.Add(otherBase);
        }
    }

    protected void OnTriggerExit(Collider collider)
    {
        WarriorBody otherBody = collider.GetComponent<WarriorBody>();

        if (otherBody != null)
        {
            Warrior otherWarrior = otherBody.transform.root.GetComponent<Warrior>();

            warriors.Remove(otherWarrior);
        }

        Base otherBase = collider.GetComponent<Base>();

        if (otherBase != null)
        {
            bases.Remove(otherBase);
        }
    }
    #endregion

    #region Warrior Access
    public bool GetEnemyWarrior(IOwnable reference, out Warrior enemy)
    {
        foreach (Warrior warrior in this.warriors)
        {
            if (warrior == null) continue;
            if( Methods.HasEnemy( reference, warrior ) )
            {
                enemy = warrior;
                return true;
            }
        }
        enemy = null;
        return false;
    }

    public List<Warrior> GetEnemyWarriors(IOwnable reference)
    {
        List<Warrior> enemies = new();

        foreach (Warrior warrior in this.warriors)
        {
            if (warrior == null) continue;
            if (Methods.HasEnemy(reference, warrior))
            {
                enemies.Add(warrior);
            }
        }

        return enemies;
    }

    public bool GetAllyWarrior(IOwnable reference, out Warrior ally)
    {
        foreach (Warrior warrior in this.warriors)
        {
            if (warrior == null) continue;
            if (Methods.HasAlly(reference, warrior))
            {
                ally = warrior;
                return true;
            }
        }

        ally = null;
        return false;
    }

    public List<Warrior> GetAllyWarriors(IOwnable reference)
    {
        List<Warrior> enemies = new();

        foreach (Warrior warrior in this.warriors)
        {
            if (warrior == null) continue;
            if (Methods.HasAlly(reference, warrior))
            {
                enemies.Add(warrior);
            }
        }

        return enemies;
    }
    #endregion

    #region Base Access
    public bool GetAllyBase(IOwnable reference, out Base ally)
    {
        foreach (Base otherBase in this.bases)
        {
            if (Methods.HasAlly(reference, otherBase))
            {
                ally = otherBase;
                return true;
            }
        }

        ally = null;
        return false;
    }

    public bool GetEnemyBase(IOwnable reference, out Base enemy)
    {
        foreach (Base otherBase in this.bases)
        {
            if (Methods.HasEnemy(reference, otherBase))
            {
                enemy = otherBase;
                return true;
            }
        }

        enemy = null;
        return false;
    }
    #endregion
}
