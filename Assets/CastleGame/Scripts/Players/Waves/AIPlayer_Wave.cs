using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AIPlayer_Wave : AIPlayerCG
{
    protected int _WaveNumber = 0;
    protected List<Wave_CG> _waves = new List<Wave_CG>();

    [SerializeField] private TextMeshProUGUI _text;

    public override void OnControlStart()
    {
        base.OnControlStart();

        for (int i = 1; i < 1000; i++)
        {
            float totalSegmentTime = 10f;
            float totalWaveTime = totalSegmentTime * 3f;
            int numEnemies = i;
            int numSpells = numEnemies * 1 / 2;
            int numDragons = i / 6;

            float spawnDelay = totalSegmentTime / numEnemies + 1;
            float spellDelay = totalSegmentTime / numSpells + 1;
            float dragonDelay =  totalWaveTime / numDragons + 1;
            float initialDelay = 5f;

            Wave_CG wave = gameObject.AddComponent<Wave_CG>();
            wave.Init(this);

            wave.AddRepeatingSegment(Prefabs_CG.instance.WarriorShield, null, numEnemies, spawnDelay, initialDelay);
            wave.AddRepeatingSegment(GetRandomSpell(), GetRandomPath(), numSpells, spellDelay, initialDelay);
            wave.SetTimeBaseFromLastSpawn(spawnDelay);

            wave.AddRepeatingSegment(Prefabs_CG.instance.WarriorSword, null, numEnemies, spawnDelay);
            wave.AddRepeatingSegment(GetRandomSpell(), GetRandomPath(), numSpells, spellDelay);
            wave.SetTimeBaseFromLastSpawn(spawnDelay);

            wave.AddRepeatingSegment(Prefabs_CG.instance.WarriorSpear,  null, numEnemies, spawnDelay);
            wave.AddRepeatingSegment(GetRandomSpell(), GetRandomPath(), numSpells, spellDelay);
            wave.SetTimeBaseFromLastSpawn(spawnDelay);

            wave.SetTimeBase(0.0f);
            wave.AddRepeatingSegment(Prefabs_CG.instance.WarriorDragon, null, numDragons, dragonDelay, dragonDelay);


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
                // increase user's maximum money, and give them full money
                UserPlayer_CG user = UserPlayers[0];
                user.MoneyCap += 4;
                user._incomeRate *= 1.10f;
                //user.Money = user.MoneyCap;

                // destroy all existing warriors
                foreach(WarriorPath path in _paths )
                {
                    for( float distFromBase = 0.01f; distFromBase <= .99f; distFromBase += .1f)
                    {
                        Meteor_Active meteor = Instantiate<Meteor_Active>(Prefabs_CG.instance.Meteor);
                        meteor.Init(this);
                        SpawnManager_CG.instance.UpdateTransform(SpawnManager_CG.SPAWN_CONTROL.FROM_BASE, meteor.transform, this, path, distFromBase);
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

    [SerializeField] EndGameManager endGameManager;
    public override void OnGameEnd(bool wonGame)
    {
        base.OnGameEnd(!wonGame);
        endGameManager.OnGameEnd(!wonGame);
    }
}
