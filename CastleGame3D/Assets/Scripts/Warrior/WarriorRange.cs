using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;

public class WarriorRange : MonoBehaviour
{
    protected List<Warrior> warriors = new List<Warrior>();
    protected List<Base> bases = new List<Base>();


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
