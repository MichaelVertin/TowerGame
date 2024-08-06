using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExit : MonoBehaviour
{
    private Projectile __owner = null;

    public void Init(Projectile projectile)
    {
        __owner = projectile;
    }

    private void OnTriggerExit(Collider other)
    {
        __owner.OnTriggerExit(other);
    }
}
