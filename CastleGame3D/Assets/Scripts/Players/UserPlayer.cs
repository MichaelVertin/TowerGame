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

    private int _money = 3;

    protected void Awake()
    {
        Money = _initialMoney;
        timer.Time = 0.0f;
    }

    private int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
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

            return true;
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
            return true;
        }
        return false;
    }
}
