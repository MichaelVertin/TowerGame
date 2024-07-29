using PlayDecalVFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static Prefabs instance;

    [SerializeField] public Spear spearPrefab;
    [SerializeField] public PlayVFX PlayVFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
