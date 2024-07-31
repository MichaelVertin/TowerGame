using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading;

public class UserPlayer : Player
{
    [SerializeField] private float _incomeRate = 1f;
    [SerializeField] private int _initialMoney = 3;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Timer timer;
    [SerializeField] private float _spellDelay = 10f;
    [SerializeField] public int MoneyCap = 20;

    private int _money = 3;

    public override void Awake()
    {
        Money = _initialMoney;
        timer.Time = 0.0f;
    }

    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = Mathf.Min(value,MoneyCap);
            _moneyText.text = Money.ToString();
        }
    }



    protected IEnumerator GetMoney()
    {
        yield return new WaitForSeconds(1f/_incomeRate);

        Money++;

        StartCoroutine(GetMoney());
    }

    public override void OnControlStart()
    {
        StartCoroutine(GetMoney());

        base.OnControlStart();
    }

    public void InitializeWarriorGeneration(WarriorFloat warriorFloatPrefab)
    {
        WarriorFloat floatingWarrior = Instantiate<WarriorFloat>(warriorFloatPrefab);
        floatingWarrior.Init(this);
    }

    public void InitializeSpellGeneration(SpellFloat spellFloatPrefab)
    {
        if( timer.Time <= 0.0f )
        {
            SpellFloat spell = Instantiate<SpellFloat>(spellFloatPrefab);
            spell.Init(this);
        }
    }

    public override bool VerifyWarriorSpawn(Warrior warrior)
    {
        int futureMoney = Money - warrior.Cost;

        if (futureMoney >= 0)
        {
            Money = futureMoney;

            return base.VerifyWarriorSpawn(warrior);
        }
        return false;
    }

    public override bool VerifySpellSpawn(Spell spell)
    {
        int futureMoney = Money - spell.Cost;

        if (futureMoney >= 0)
        {
            Money = futureMoney;
            timer.Time += _spellDelay;
            return base.VerifySpellSpawn(spell);
        }
        return false;
    }
}
