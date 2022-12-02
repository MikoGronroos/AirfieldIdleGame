using UnityEngine;

public class Projectile : PoolableObject
{

    [field: SerializeField] public bool HasBeenHit { get; set; } = false;

}
