using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AIPlayer : Player
{
    [SerializeField] protected Meteor _meteorPrefab;
    [SerializeField] public List<UserPlayer> UserPlayers;

    public List<Path> _paths;

    protected override void Start()
    {
        _paths = new List<Path>(SpawnManager.instance.BaseTransforms[this].Keys);
        _paths = new List<Path>();
        foreach( var path in SpawnManager.instance.BaseTransforms[this].Keys )
        {
            _paths.Add(path);
        }
        base.Start();
    }

    public void SpawnAfterTime(float time, Warrior warrior, Path path)
    {
        StartCoroutine(SpawnAfterTime_IEnum(time, warrior, path));
    }

    public IEnumerator SpawnAfterTime_IEnum(float time, Warrior warrior, Path path)
    {
        yield return new WaitForSeconds(time);
        Spawn(warrior, path);
    }

    public void Spawn(Warrior warrior, Path path)
    {
        if( path != null)
        {
            SpawnManager.instance.SpawnWarriorOnPath(this, path, warrior);
        }
        else
        {
            foreach(Path pathIter in _paths)
            {
                Spawn(warrior, pathIter);
            }
        }
    }

    public Spell SpawnRandomSpell()
    {
        Path randomPath = GetRandomPath();

        Meteor spawnedSpell = null;
        float randomPosition = Random.value;

        Transform spawnTrans =SpawnManager.instance.BaseTransforms[this][randomPath];
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
