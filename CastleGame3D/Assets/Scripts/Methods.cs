using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Methods
{
    public static bool ShareOwner(IOwnable one, IOwnable other)
    {
        return one.Owner == other.Owner;
    }

    public static bool HasEnemy(IOwnable one, IOwnable other)
    {
        return !ShareOwner(one, other);
    }

    public static bool HasAlly(IOwnable one, IOwnable other)
    {
        return ShareOwner(one, other);
    }
}
