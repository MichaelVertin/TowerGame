using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: find a way to combine WarriorRange and WeaponRange into one class
public class ProjectileRange : MonoBehaviour
{
    public List<Warrior> warriors = new List<Warrior>();
    public List<Base> bases = new List<Base>();


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
}
