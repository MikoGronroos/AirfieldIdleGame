using UnityEngine;

public class ProjectileBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            ProjectileManager.Instance.RemoveProjectile(collision.GetComponent<Projectile>());
        }
    }
}
