using UnityEngine;

public class Plane : PoolableObject
{

    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;

    [SerializeField] private float speed;

    [SerializeField] private int currencyOnDeath;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Projectile projectile))
        {

            if (projectile.HasBeenHit) return;

            projectile.DestroyedAPlane();
            projectile.HasBeenHit = true;
            CurrencyManager.Instance.CurrentCurrency += currencyOnDeath;
            PlaneManager.Instance.RemovePlane(this);
            ProjectileManager.Instance.RemoveProjectile(projectile);
        }
    }
}
