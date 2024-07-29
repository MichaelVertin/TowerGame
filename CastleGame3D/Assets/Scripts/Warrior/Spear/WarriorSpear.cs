using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarriorSpear : Warrior
{
    [SerializeField] private Vector3 _launchDirection = new Vector3(0.0f,1.0f,0.0f);
    private GameObject __spearParentObj;

    public override void OnAttack()
    {
        Spear spear = GetComponentInChildren<Spear>();
        if( spear != null)
        {
            __spearParentObj = spear.gameObject.transform.parent.gameObject;

            spear.launchSpear(this, _launchDirection);
        }
    }

    public override void OnAttackEnd()
    {
        base.OnAttackEnd();
        Spear spear = Instantiate<Spear>(Prefabs.instance.spearPrefab, __spearParentObj.transform);
    }
}
