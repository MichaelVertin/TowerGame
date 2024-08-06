using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOwnable
{
    public Player Owner { get; set; }

    /* requires explicit typecast - moved to static class 'Methods'
    public bool HasOwner(IOwnable ownable)
    {
        return this.Owner == ownable.Owner;
    }

    public bool HasEnemy(IOwnable ownable)
    {
        return this.Owner != ownable.Owner;
    }

    public bool HasAlly(IOwnable ownable)
    {
        return HasOwner(ownable);
    }
    */
}
