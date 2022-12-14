using System;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class Turret : MonoBehaviour
{

    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float currentTime;

    [SerializeField] private Item projectileItem;

    [SerializeField] private bool shootFromRandomBarrels;

    [SerializeField] private int index;
    [SerializeField] private Transform[] turretBarrels;

    private bool _goingUp = true;

    [field: SerializeField] public string Id { get; set; }

    private void Start()
    {
        currentTime = fireRate;
    }

    public void UpdateTick()
    {

        currentTime = Mathf.Clamp(currentTime -= 1 * Time.deltaTime, 0, fireRate);

        if (currentTime <= 0)
        {

            if (shootFromRandomBarrels)
            {
                index = UnityEngine.Random.Range(0, turretBarrels.Count());
            }else if(turretBarrels.Count() == 1)
            {
                index = 0;
            }
            else
            {
                if (_goingUp)
                {
                    index++;
                    if (index <= turretBarrels.Count() - 1) _goingUp = false;
                }
                else
                {
                    index--;
                    if (index <= 0) _goingUp = true;
                }
            }

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

    private void OnMouseDown()
    {
        MergeManager.Instance.SelectedTurret = this;
    }

    [ContextMenu("Generate new id")]
    public void GenerateId() => Id = Guid.NewGuid().ToString();

}
