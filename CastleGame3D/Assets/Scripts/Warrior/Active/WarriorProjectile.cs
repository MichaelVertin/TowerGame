using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarriorProjectile : Warrior
{
    [SerializeField] private Vector3 _launchDirection = new Vector3(0.0f,1.0f,0.0f);
    private GameObject __projectileParentObj;
    protected Projectile _projectile;

    protected override void Awake()
    {
        base.Awake();
        _projectile = GetComponentInChildren<Projectile>();
        __projectileParentObj = _projectile.gameObject.transform.parent.gameObject;
        _projectile.Init(RangeOfEffect);
    }

    public override void OnAttack()
    {
        _projectile.launch(this, _launchDirection);
    }

    public override void OnAttackEnd()
    {
        base.OnAttackEnd();
        _projectile = Instantiate<Projectile>(Prefabs.instance.Spear, __projectileParentObj.transform);
        _projectile.Init(RangeOfEffect);
    }
}
