using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Range;

public class WarriorBody : MonoBehaviour
{
    public Warrior Warrior
    {
        get
        {
            return GetComponentInParent<Warrior>();
        }
    }
    protected BoxCollider rangeCollider;

    protected void Awake()
    {
        SetCollider();
    }

    #region AccessType
    [SerializeField] protected AccessType accessType = AccessType.GROUND;

    public virtual void SetCollider()
    {
        rangeCollider = GetComponent<BoxCollider>();
        if (rangeCollider == null)
        {
            Debug.LogError("Unable to identify Box Collider for Range");
        }
        Vector3 colliderCenter = rangeCollider.center;
        Vector3 colliderSize = rangeCollider.size;

        // x is always the same
        // y is dependent on AccessType
        // z is defined by user
        colliderCenter.x = 0;
        colliderSize.x = 8;

        if (accessType == AccessType.FLYING)
        {
            colliderCenter.y = 6 + 1 + 3;
            colliderSize.y = 6 * .9f;
        }
        else if (accessType == AccessType.GROUND_AND_FLYING)
        {
            colliderCenter.y = 6 + 1 / 2f;
            colliderSize.y = ( 6 * 2 + 1 ) * .95f;
        }
        else if (accessType == AccessType.GROUND)
        {
            colliderCenter.y = 3;
            colliderSize.y = 6 * .95f;
        }
        else
        {
            Debug.LogError("Unrecognized AccessType");
        }

        rangeCollider.center = colliderCenter;
        rangeCollider.size = colliderSize;
    }
    #endregion
}
