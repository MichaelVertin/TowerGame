using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AIPlayer_Wave : AIPlayer
{
    protected int _WaveNumber = 0;
    protected List<Wave> _waves = new List<Wave>();

    [SerializeField] private TextMeshProUGUI _text;

    public override void OnControlStart()
    {
        base.OnControlStart();

        for (int i = 1; i < 20; i++)
        {
            float spawnDelay = 10f / i + 2;
            float initialDelay = 5f;

            Wave wave = gameObject.AddComponent<Wave>();
            wave.Init(this);
            wave.AddRepeatingSegment(_shieldWarrior, null, i, spawnDelay, initialDelay);
            wave.SetTimeBaseFromLastSpawn(spawnDelay);

            wave.AddRepeatingSegment(_swordWarrior,  null, i, spawnDelay);
            wave.SetTimeBaseFromLastSpawn(spawnDelay);

            wave.AddRepeatingSegment(_spearWarrior,  null, i, spawnDelay);
            wave.SetTimeBaseFromLastSpawn(spawnDelay);

            _waves.Add(wave);
        }

        PlayWave();
    }

    protected virtual bool PlayWave()
    {
        _text.text = "Waves: " + _WaveNumber;
        if ( _WaveNumber < _waves.Count )
        {
            _waves[ _WaveNumber ].Play();
            _WaveNumber++;

            // between waves:
            if( _WaveNumber != 1 )
            {
                // increaseuser's maximum money, and give them full money
                UserPlayer user = UserPlayers[0];
                user.MoneyCap += 2;
                //user.Money = user.MoneyCap;

                // destroy all existing warriors
                foreach(Path path in _paths )
                {
                    for( float distFromBase = 0.01f; distFromBase <= .99f; distFromBase += .1f)
                    {
                        Meteor meteor = Instantiate<Meteor>(_meteorPrefab);
                        meteor.Init(this);
                        SpawnManager.instance.UpdateTransformForPath(meteor.transform, this, path, distFromBase);
                    }
                }
            }

            return true;
        }
        return false;
    }

    protected override void FixedUpdate()
    {
        // Play current wave if the last wave ended and there are no remaining warriors
        if(_waves[_WaveNumber-1] == null)
        {
            bool foundWarrior = false;
            foreach (Warrior warrior in Warriors)
            {
                if (warrior != null)
                {
                    foundWarrior = true;
                }
            }
            if (!foundWarrior)
            {
                PlayWave();
            }
        }

        base.FixedUpdate();

    }

    protected IEnumerator PlayWave_Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayWave();
    }
}
