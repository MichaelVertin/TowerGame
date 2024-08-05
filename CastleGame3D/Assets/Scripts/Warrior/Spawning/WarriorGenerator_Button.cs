using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarriorGenerator_Button : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] UserPlayer player;
    [SerializeField] WarriorFloat warrior;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.InitializeWarriorGeneration(warrior);
    }
}
