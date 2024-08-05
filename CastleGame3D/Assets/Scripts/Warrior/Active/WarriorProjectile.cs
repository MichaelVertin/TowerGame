using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarriorProjectile : Warrior
{
    [SerializeField] private Vector3 _launchDirection = new Vector3(0.0f,1.0f,0.0f);
    private GameObject __projectileParentObj;

    public override void OnAttack()
    {
        Projectile projectile = GetComponentInChildren<Projectile>();
        if( projectile != null)
        {
            __projectileParentObj = projectile.gameObject.transform.parent.gameObject;

            projectile.launch(this, _launchDirection);
        }
    }

    public override void OnAttackEnd()
    {
        base.OnAttackEnd();
        Projectile projectile = Instantiate<Projectile>(Prefabs.instance.Spear, __projectileParentObj.transform);
    }
}
