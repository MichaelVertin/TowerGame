using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static SpawnManager;

public class AIPlayer : Player
{
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
            SpawnManager.instance.Spawn(warrior, SPAWN_CONTROL.FROM_BASE, this, path);
        }
        else
        {
            foreach(Path pathIter in _paths)
            {
                Spawn(warrior, pathIter);
            }
        }
    }

    public void Spawn(SpellActive spell, Path path)
    {
        if (path != null)
        {
            SpawnManager.instance.Spawn(spell, SPAWN_CONTROL.RANDOM, this, path);
        }
        else
        {
            foreach (Path pathIter in _paths)
            {
                Spawn(spell, pathIter);
            }
        }
    }

    public SpellActive SpawnRandomSpell()
    {
        Path randomPath = GetRandomPath();
        SpellActive activeSpell = GetRandomSpell();

        return SpawnManager.instance.Spawn(activeSpell, SPAWN_CONTROL.RANDOM, this, randomPath);
    }

    protected Path GetRandomPath()
    {
        return _paths[Random.Range(0, _paths.Count)];
    }

    public SpellActive GetRandomSpell()
    {
        List<SpellActive> spells = Prefabs.instance.Spells;
        return spells[Random.Range(0, spells.Count)];
    }
}
