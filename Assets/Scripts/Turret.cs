using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float currentTime;

    [SerializeField] private Item projectileItem;

    [SerializeField] private int index;
    [SerializeField] private Transform[] turretBarrels;

    private void Start()
    {
        currentTime = fireRate;
    }

    public void UpdateTick()
    {

        currentTime = Mathf.Clamp(currentTime -= 1 * Time.deltaTime, 0, fireRate);

        if (currentTime <= 0)
        {
            index = index == 0 ? 1 : 0;

            if (Stockpile.Instance.RemoveFromStockpile(projectileItem, 1))
            {
                Projectile newProjectile = ProjectileManager.Instance.SpawnProjectile();
                newProjectile.transform.position = turretBarrels[index].position;
                newProjectile.transform.rotation = turretBarrels[index].rotation;
                newProjectile.HasBeenHit = false;

                newProjectile.GetComponent<Rigidbody2D>().AddForce(newProjectile.transform.up * projectileSpeed);

                currentTime = fireRate;
            }
        }

    }

}
