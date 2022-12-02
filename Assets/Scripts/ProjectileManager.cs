using UnityEngine;

public class ProjectileManager : MonoBehaviour
{


    [SerializeField] private ObjectPool pool;

    #region Singleton

    private static ProjectileManager _instance;

    public static ProjectileManager Instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    public Projectile SpawnProjectile()
    {
        return pool.Get() as Projectile;
    }

    public void RemoveProjectile(Projectile projectile)
    {
        pool.Release(projectile);
    }

}
