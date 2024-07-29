using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TriggerScript : MonoBehaviour
{
    [SerializeField] Meteor meteor;
    private void OnTriggerEnter(Collider other)
    {
        meteor.OnTriggerEnter(other);
    }
}
