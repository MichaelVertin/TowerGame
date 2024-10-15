using PlayDecalVFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static Prefabs instance;

    [SerializeField] public Projectile Spear;

    [SerializeField] public Warrior WarriorShield;
    [SerializeField] public Warrior WarriorSword;
    [SerializeField] public Warrior WarriorSpear;
    [SerializeField] public Warrior WarriorDragon;

    [SerializeField] public Meteor_Active Meteor;
    [SerializeField] public Haste_Active Haste;
    [SerializeField] public Freeze_Active Freeze;

    [SerializeField] public List<Warrior> Warriors;
    [SerializeField] public List<SpellActive> Spells;

    [SerializeField] public Freeze freeze2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
