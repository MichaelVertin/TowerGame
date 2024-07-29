using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearExit : MonoBehaviour
{
    private Spear __owner = null;

    public void init(Spear spear)
    {
        __owner = spear;
    }

    private void OnTriggerExit(Collider other)
    {
        __owner.OnTriggerExit(other);
    }
}
