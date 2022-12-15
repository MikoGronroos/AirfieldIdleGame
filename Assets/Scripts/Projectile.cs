using System;
using UnityEngine;

public class Projectile : PoolableObject
{

    [field: SerializeField] public bool HasBeenHit { get; set; } = false;

    public Action DestroyedAPlaneCallback { get; set; }

    public void DestroyedAPlane()
    {
        DestroyedAPlaneCallback();
    }

}
