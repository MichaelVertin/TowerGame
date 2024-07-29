using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBody : MonoBehaviour
{
    public Warrior Warrior
    {
        get
        {
            return GetComponentInParent<Warrior>();
        }
    }
}
