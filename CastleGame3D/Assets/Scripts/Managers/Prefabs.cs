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

    [SerializeField] public Meteor Meteor;
    [SerializeField] public Haste Haste;
    [SerializeField] public Freeze Freeze;

    [SerializeField] public List<Warrior> Warriors;
    [SerializeField] public List<SpellActive> Spells;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
