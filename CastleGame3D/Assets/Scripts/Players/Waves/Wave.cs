using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct WaveSegment_Repeat
{
    public int Count;
    public float Spacing;
    public float Start;
    public Warrior Warrior;
    public Path Path;
    public SpellActive Spell;

    public WaveSegment_Repeat(Warrior warrior, Path path, int count, float spacing, float start)
    {
        Count = count;
        Spacing = spacing;
        Warrior = warrior;
        Spell = null;
        Start = start;
        Path = path;
    }

    public WaveSegment_Repeat(SpellActive spell, Path path, int count, float spacing, float start)
    {
        Count = count;
        Spacing = spacing;
        Warrior = null;
        Spell = spell;
        Start = start;
        Path = path;
    }
}



public class Wave : MonoBehaviour
{
    private AIPlayer owner = null;
    private List<WaveSegment_Repeat> waveSegmentRepeats = new List<WaveSegment_Repeat>();
    private int remainingSpawns = 0;

    // Base time
    private float TimeBase = 0.0f;
    private float LastSpawn = 0.0f;

    public void AddRepeatingSegment(Warrior warrior, Path path, int count = 1, float spacing = 0.0f, float start = 0.0f)
    {
        // adjust start from the base
        start += TimeBase;

        WaveSegment_Repeat repeat = new WaveSegment_Repeat(warrior, path, count, spacing, start);
        waveSegmentRepeats.Add(repeat);
        remainingSpawns += count;

        // recalculate LastSpawn time
        LastSpawn = Mathf.Max(LastSpawn, start + (count-1)*spacing);
    }

    public void AddRepeatingSegment(SpellActive spell, Path path, int count = 1, float spacing = 0.0f, float start = 0.0f)
    {
        // adjust start from the base
        start += TimeBase;

        WaveSegment_Repeat repeat = new WaveSegment_Repeat(spell, path, count, spacing, start);
        waveSegmentRepeats.Add(repeat);
        remainingSpawns += count;

        // recalculate LastSpawn time
        LastSpawn = Mathf.Max(LastSpawn, start + (count - 1) * spacing);
    }

    public void Init(AIPlayer owner)
    {
        this.owner = owner;
    }

    public void Play()
    {
        foreach(WaveSegment_Repeat waveSegment in waveSegmentRepeats)
        {
            StartCoroutine(PlayRepeatingSegment(waveSegment));
        }
    }

    public IEnumerator PlayRepeatingSegment(WaveSegment_Repeat waveSegment)
    {
        yield return new WaitForSeconds(waveSegment.Start);

        for(int segmentInd = 0; segmentInd < waveSegment.Count; segmentInd++ )
        {
            if( waveSegment.Warrior != null )
            {
                owner.Spawn(waveSegment.Warrior, waveSegment.Path);
            }
            if( waveSegment.Spell != null )
            {
                owner.Spawn(waveSegment.Spell, waveSegment.Path);
            }

            yield return new WaitForSeconds(waveSegment.Spacing);
            remainingSpawns -= 1;
        }

        if( remainingSpawns <= 0 )
        {
            Destroy(this);
        }

        yield return null;
    }

    public void SetTimeBase(float timeFromWaveStart=0.0f)
    {
        TimeBase = timeFromWaveStart;
    }

    public void AdjustTimeBase(float adjust)
    {
        TimeBase += adjust;
    }

    public void SetTimeBaseFromLastSpawn(float adjust=0.0f)
    {
        TimeBase = LastSpawn + adjust;
    }
}
