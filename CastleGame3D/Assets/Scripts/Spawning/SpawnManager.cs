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


public class SpawnManager : MonoBehaviour
{
    // stores dictionary of all Transforms where Players can interact with the path
    public Dictionary<Player, Dictionary<Path, Transform>> BaseTransforms = new Dictionary<Player, Dictionary<Path, Transform>>();

    // list of all players and paths
    [SerializeField] private List<Player> players = new List<Player>();
    [SerializeField] private List<Path> paths = new List<Path>();
    [SerializeField] public float PATH_HEIGHT = 1.0f;

    public static SpawnManager instance;

    private void Awake()
    {
        if( instance != null )
        {
            Destroy(this.gameObject);
            Debug.Log("Attempted to create a second SpawnManager class");
        }

        instance = this;


        // initialize BaseTransforms
        foreach( Player player in players )
        {
            BaseTransforms[player] = new Dictionary<Path, Transform>();
            foreach(Path path in paths)
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

    public Warrior SpawnWarriorOnPath(Player owner, Path path, Warrior warrior, float pathProgression = 0.0f)
    {
        Warrior spawnedWarrior = null;
        if(VerifyPlayerPath(owner, path) && owner.VerifyWarriorSpawn(warrior))
        {
            Transform spawnTrans = BaseTransforms[owner][path];
            spawnedWarrior = Instantiate<Warrior>(warrior);
            spawnedWarrior.Init(owner);
            UpdateTransformForPath(spawnedWarrior.transform, owner, path, pathProgression);
        }
        return spawnedWarrior;
    }

    // TODO: replace Meteor with Interface 'Initializable'
    public SpellActive SpawnSpellOnPath(Player owner, Path path, SpellActive spellPrefab)
    {
        SpellActive spawnedSpell = null;

        if(VerifyPlayerPath(owner,path) && owner.VerifySpellSpawn(spellPrefab))
        {
            Transform spawnTrans = BaseTransforms[owner][path];
            spawnedSpell = Instantiate<SpellActive>(spellPrefab);
            spawnedSpell.Init(owner);
            UpdateTransformForCursor(spawnedSpell.transform, owner, path);
        }

        return spawnedSpell;
    }

    public bool VerifyPlayerPath(Player player, Path path)
    {
        return BaseTransforms.ContainsKey(player) && BaseTransforms[player].ContainsKey(path);
    }

    public bool UpdateTransformForPath(Transform trans, Player owner, Path path, float distFromBase = 0.0f)
    {
        if( distFromBase < 0.0f || distFromBase > 1.0f )
        {
            Debug.LogError("must set distFromBase between 0 and 1");
        }
        if(VerifyPlayerPath(owner,path))
        {
            Transform baseTrans = BaseTransforms[owner][path];
            Vector3 pos = baseTrans.position;
            pos.x *= 1 - 2 * distFromBase;
            pos.y += PATH_HEIGHT;
            trans.SetPositionAndRotation(pos, baseTrans.rotation);
            return true;
        }
        return false;
    }

    public bool UpdateTransformForCursor(Transform trans, Player owner, Path path)
    {
        if (VerifyPlayerPath(owner, path))
        {
            if( InputManager.GetPointUnderMouse<Path>(out Vector3 cursorPos) )
            {
                Transform baseTrans = BaseTransforms[owner][path];
                // center cursor position
                cursorPos.z = baseTrans.position.z;
                cursorPos.y += PATH_HEIGHT;
                trans.SetPositionAndRotation(cursorPos, baseTrans.rotation);
                return true;
            }
        }
        return false;
    }
}
