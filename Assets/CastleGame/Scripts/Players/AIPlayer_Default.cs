using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIPlayer_Default : AIPlayerCG
{
    [SerializeField] private float _spawnDelay = 3f;
    [SerializeField] private float _spawnRateMult = .99f;

    [SerializeField] private float _spellDelay = 10f;
    [SerializeField] private float _spellRateMult = 1f;

    [SerializeField] protected float _shieldSpawnDelay;
    [SerializeField] protected float _swordSpawnDelay;
    [SerializeField] protected float _spearSpawnDelay;

    public override void OnControlStart()
    {
        StartCoroutine(temp());
        StartCoroutine(temp2());

        base.OnControlStart();
    }
    
    public IEnumerator temp()
    {
        foreach (WarriorPath path in _paths)
        {
            SpawnAfterTime(_spawnDelay * _shieldSpawnDelay, Prefabs_CG.instance.WarriorShield, path);
            SpawnAfterTime(_spawnDelay * _swordSpawnDelay, Prefabs_CG.instance.WarriorSword, path);
            SpawnAfterTime(_spawnDelay * _spearSpawnDelay, Prefabs_CG.instance.WarriorSpear, path);
        }

        _spawnDelay *= _spawnRateMult;

        yield return new WaitForSeconds(_spawnDelay);

        StartCoroutine(temp());
    }

    public IEnumerator temp2()
    {
        yield return new WaitForSeconds(_spellDelay);

        SpawnRandomSpell();

        _spellDelay *= _spellRateMult;

        StartCoroutine(temp2());
    }
}
