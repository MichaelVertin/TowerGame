using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static SpawnManager_CG;

public class AIPlayerCG : Player_CG
{
    [SerializeField] public List<UserPlayer_CG> UserPlayers;

    public List<WarriorPath> _paths;

    protected override void Start()
    {
        _paths = new List<WarriorPath>(SpawnManager_CG.instance.BaseTransforms[this].Keys);
        _paths = new List<WarriorPath>();
        foreach( var path in SpawnManager_CG.instance.BaseTransforms[this].Keys )
        {
            _paths.Add(path);
        }
        base.Start();
    }

    public void SpawnAfterTime(float time, Warrior warrior, WarriorPath path)
    {
        StartCoroutine(SpawnAfterTime_IEnum(time, warrior, path));
    }

    public IEnumerator SpawnAfterTime_IEnum(float time, Warrior warrior, WarriorPath path)
    {
        yield return new WaitForSeconds(time);
        Spawn(warrior, path);
    }

    public void Spawn(Warrior warrior, WarriorPath path)
    {
        if( path != null)
        {
            SpawnManager_CG.instance.Spawn(warrior, SPAWN_CONTROL.FROM_BASE, this, path);
        }
        else
        {
            foreach(WarriorPath pathIter in _paths)
            {
                Spawn(warrior, pathIter);
            }
        }
    }

    public void Spawn(SpellActive spell, WarriorPath path)
    {
        if (path != null)
        {
            SpawnManager_CG.instance.Spawn(spell, SPAWN_CONTROL.RANDOM, this, path);
        }
        else
        {
            foreach (WarriorPath pathIter in _paths)
            {
                Spawn(spell, pathIter);
            }
        }
    }

    public SpellActive SpawnRandomSpell()
    {
        WarriorPath randomPath = GetRandomPath();
        SpellActive activeSpell = GetRandomSpell();

        return SpawnManager_CG.instance.Spawn(activeSpell, SPAWN_CONTROL.RANDOM, this, randomPath);
    }

    protected WarriorPath GetRandomPath()
    {
        return _paths[Random.Range(0, _paths.Count)];
    }

    public SpellActive GetRandomSpell()
    {
        List<SpellActive> spells = Prefabs_CG.instance.Spells;
        return spells[Random.Range(0, spells.Count)];
    }
}
