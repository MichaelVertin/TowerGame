using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellGenerator_Button1 : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] UserPlayer player;
    [SerializeField] SpellStandard spell;

    public void OnPointerDown(PointerEventData eventData)
    {
        //player.InitializeSpellGeneration(spellStandard);
        SpellStandard generatedSpell = Instantiate<SpellStandard>(spell);
        generatedSpell.Init(player);
    }
}
