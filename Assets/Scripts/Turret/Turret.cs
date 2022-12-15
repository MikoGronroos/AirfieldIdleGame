using System;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float currentTime;

    [SerializeField] private int planesShotDown;

    [SerializeField] private Item projectileItem;

    [SerializeField] private bool shootFromRandomBarrels;

    [SerializeField] private int index;
    [SerializeField] private Transform[] turretBarrels;

    [SerializeField] private Turret[] possibleMerges;

    private bool _goingUp = true;

    public string UniqueId { get; set; }

    [field: SerializeField] public GeneralTurretInfo TurretInfo { get; private set; }

    private void Start()
    {
        currentTime = fireRate;
    }

    private void OnEnable()
    {
        TurretManager.Instance.AddTurret(this);
        UniqueId = Guid.NewGuid().ToString();
    }

    private void OnDisable()
    {
        TurretManager.Instance.RemoveTurret(this);
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
                newProjectile.DestroyedAPlaneCallback = ProjectileShotDownAPlane;
                newProjectile.transform.position = turretBarrels[index].position;
                newProjectile.transform.rotation = turretBarrels[index].rotation;
                newProjectile.HasBeenHit = false;

                newProjectile.GetComponent<Rigidbody2D>().AddForce(newProjectile.transform.up * projectileSpeed);

                currentTime = fireRate;
            }
        }

    }

    private void ProjectileShotDownAPlane()
    {
        UIEffectManager.Instance.StartUIEffect(UIEffect.KillConfirmed, transform.position);
        planesShotDown++;
    }

    public void StartDrag()
    {
        MergeManager.Instance.SelectedTurret = this;
    }

    public Turret[] GetPossibleMerges()
    {
        return possibleMerges;
    }

    public int GetPlanesShotDown()
    {
        return planesShotDown;
    }

}
