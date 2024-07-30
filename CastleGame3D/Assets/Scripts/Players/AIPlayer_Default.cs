using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class AIPlayer_Default : AIPlayer
{
    [SerializeField] private float _spawnDelay = 3f;
    [SerializeField] private float _spawnRateMult = .99f;

    [SerializeField] private float _spellDelay = 10f;
    [SerializeField] private float _spellRateMult = 1f;

    public override void Awake()
    {
        _paths = new List<Path>(SpawnManager.instance.BaseTransforms[this].Keys);
    }

    public override void OnControlStart()
    {
        StartCoroutine(temp());
        StartCoroutine(temp2());

        base.OnControlStart();
    }

    public IEnumerator temp()
    {
        foreach (Path path in _paths)
        {
            StartCoroutine(SpawnAfterTime(_spawnDelay * _shieldSpawnDelay, _shieldWarrior, path));
            StartCoroutine(SpawnAfterTime(_spawnDelay * _swordSpawnDelay, _swordWarrior, path));
            StartCoroutine(SpawnAfterTime(_spawnDelay * _spearSpawnDelay, _spearWarrior, path));
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

    public IEnumerator SpawnAfterTime(float time, Warrior warrior, Path path)
    {
        yield return new WaitForSeconds(time);

        SpawnManager.instance.SpawnWarriorOnPath(this, path, warrior);
    }

    public Spell SpawnRandomSpell()
    {
        Path randomPath = GetRandomPath();

        Meteor spawnedSpell = null;
        float randomPosition = Random.value;

        Transform spawnTrans = SpawnManager.instance.BaseTransforms[this][randomPath];
        spawnedSpell = Instantiate<Meteor>(_meteorPrefab);
        spawnedSpell.Init(this);
        SpawnManager.instance.UpdateTransformForPath(spawnedSpell.transform, this, randomPath, randomPosition);

        return spawnedSpell;
    }

    private Path GetRandomPath()
    {
        return _paths[Random.Range(0, _paths.Count)];
    }
}
*/