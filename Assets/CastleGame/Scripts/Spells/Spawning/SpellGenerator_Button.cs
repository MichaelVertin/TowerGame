using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellGenerator_Button : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] UserPlayer_CG player;
    [SerializeField] SpellFloat spellFloat;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.InitializeSpellGeneration(spellFloat);
    }
}
