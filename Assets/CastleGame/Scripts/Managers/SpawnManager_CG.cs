using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class serializableClass<DataType>
{
    public List<DataType> sampleList;
}


public class SpawnManager_CG : MonoBehaviour
{
    public enum SPAWN_CONTROL
    {
        FROM_CURSOR, 
        FROM_BASE, 
        RANDOM
    }

    // stores dictionary of all Transforms where Players can interact with the path
    public Dictionary<Player_CG, Dictionary<WarriorPath, Transform>> BaseTransforms = new Dictionary<Player_CG, Dictionary<WarriorPath, Transform>>();

    // list of all players and paths
    [SerializeField] private List<Player_CG> players = new List<Player_CG>();
    [SerializeField] private List<WarriorPath> paths = new List<WarriorPath>();
    [SerializeField] public float PATH_HEIGHT = 1.0f;

    public static SpawnManager_CG instance;

    private void Awake()
    {
        if( instance != null )
        {
            Destroy(this.gameObject);
            Debug.LogError("Attempted to create a second SpawnManager class");
        }

        instance = this;


        // initialize BaseTransforms
        foreach( Player_CG player in players )
        {
            BaseTransforms[player] = new Dictionary<WarriorPath, Transform>();
            foreach(WarriorPath path in paths)
            {
                foreach(Transform playerTrans in player._spawnTransforms)
                {
                    foreach(Transform pathTrans in path.spawnPoints)
                    {
                        if( playerTrans == pathTrans )
                        {
                            BaseTransforms[player][path] = playerTrans;
                        }
                    }
                }
            }
        }
    }

    #region Spawning
    public Warrior Spawn(Warrior warriorPrefab, SPAWN_CONTROL spawnControl, Player_CG owner, WarriorPath path = null, float pathProgression = 0.0f)
    {
        // create spell
        Warrior spawnedWarrior = Instantiate<Warrior>(warriorPrefab);
        spawnedWarrior.Init(owner);

        // check able to identify the spawn transform and the owner approves
        if (UpdateTransform(spawnControl, spawnedWarrior.transform, owner, path, pathProgression) &&
           owner.VerifySpawn(spawnedWarrior))
        {
            // add spell to owner's spells
            owner.Warriors.Add(spawnedWarrior);
        }
        // if the transform cannot be found or the owner didn't approve, destroy the spell
        else
        {
            Destroy(spawnedWarrior.gameObject);
            spawnedWarrior = null;
        }

        return spawnedWarrior;
    }

    public SpellActive Spawn(SpellActive spellPrefab, SPAWN_CONTROL spawnControl, Player_CG owner, WarriorPath path = null, float pathProgression = 0.0f)
    {
        // create spelll
        SpellActive spawnedSpell = Instantiate<SpellActive>(spellPrefab);
        spawnedSpell.Init(owner);

        // check able to identify the spawn transform and the owner approves
        if(UpdateTransform(spawnControl, spawnedSpell.transform, owner, path, pathProgression) &&
           owner.VerifySpawn(spawnedSpell))
        {
            // add spell to owner's spells
            owner.Spells.Add(spawnedSpell);
        }
        // if the transform cannot be found or the owner didn't approve, destroy the spell
        else
        {
            Destroy(spawnedSpell.gameObject);
            spawnedSpell = null;
        }

        return spawnedSpell;
    }

    public SpellStandard Spawn(SpellStandard spellPrefab, SPAWN_CONTROL spawnControl, Player_CG owner, WarriorPath path = null, float pathProgression = 0.0f)
    {
        // create spell
        //SpellActive spawnedSpell = Instantiate<SpellActive>(spellPrefab);
        SpellStandard spawnedSpell = spellPrefab;

        // check able to identify the spawn transform and the owner approves
        if (UpdateTransform(spawnControl, spawnedSpell.transform, owner, path, pathProgression) &&
            owner.VerifySpawn(spawnedSpell))
        {
            // add spell to owner's spells
            spellPrefab.Cast();
            owner.Spells.Add(spawnedSpell);
        }
        // if the transform cannot be found or the owner didn't approve, destroy the spell
        else
        {
            Destroy(spawnedSpell.gameObject);
            spawnedSpell = null;
        }

        return spawnedSpell;
    }
    #endregion

    #region Transform
    // updates transform parameter using spawnControl
    //  - FROM_CURSOR: sets at the point of the cursor (path/distFromBase not used)
    //  - FROM_BASE: sets at the base, distFromBase is the distance away from the base (0-1)
    //  - RANDOM: sets at a random position between both bases (distFromBase not used)
    public bool UpdateTransform( SPAWN_CONTROL spawnControl, Transform trans, Player_CG owner, WarriorPath path = null, float distFromBase = 0.0f)
    {
        if( spawnControl == SPAWN_CONTROL.FROM_CURSOR )
        {
            if( InputManager.GetObjectUnderMouse<WarriorPath>(out path, out Vector3 cursorPos) && VerifyPlayerPath(owner, path))
            {
                Transform baseTrans = BaseTransforms[owner][path];
                // center cursor position
                cursorPos.z = baseTrans.position.z;
                cursorPos.y += PATH_HEIGHT;
                trans.SetPositionAndRotation(cursorPos, baseTrans.rotation);
                return true;
            }
        }
        else if(spawnControl == SPAWN_CONTROL.FROM_BASE)
        {
            if(VerifyPlayerPath(owner, path))
            {
                if (distFromBase < -0.00001f || distFromBase > 1.000001f)
                {
                    Debug.LogError("must set distFromBase between 0 and 1");
                }
                if (VerifyPlayerPath(owner, path))
                {
                    Transform baseTrans = BaseTransforms[owner][path];
                    Vector3 pos = baseTrans.position;
                    pos.x *= 1 - 2 * distFromBase;
                    pos.y += PATH_HEIGHT;
                    trans.SetPositionAndRotation(pos, baseTrans.rotation);
                    return true;
                }
            }
        }
        else if(spawnControl == SPAWN_CONTROL.RANDOM)
        {
            float randDist = Random.value;
            return UpdateTransform(SpawnManager_CG.SPAWN_CONTROL.FROM_BASE, trans, owner, path, randDist);
        }
        else
        {
            Debug.LogError("Invalid SPAWN_CONTROL (missing case in SpawnManager.UpdateTransform())");
        }

        return false;
    }
    #endregion


    // checks if player can spawn from the path
    public bool VerifyPlayerPath(Player_CG player, WarriorPath path)
    {
        if( player == null || path == null ) return false;

        return BaseTransforms.ContainsKey(player) && BaseTransforms[player].ContainsKey(path);
    }
}
