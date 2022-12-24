using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretManager : MonoBehaviour
{

    [SerializeField] private List<Turret> turrets = new List<Turret>();

    #region Singleton

    private static TurretManager _instance;

    public static TurretManager Instance { get { return _instance; } }

    #endregion

    private void Awake()
    {
        _instance = this;
        turrets = FindObjectsOfType<Turret>().ToList();
    }

    private void Update()
    {
        foreach (var turret in turrets)
        {
            turret.UpdateTick();
        }
    }

    public void AddTurret(Turret turret)
    {
        Airfield.Instance.PowerScore += turret.TurretInfo.PowerScore;
        turrets.Add(turret);
    }

    public void RemoveTurret(Turret turret)
    {
        Airfield.Instance.PowerScore -= turret.TurretInfo.PowerScore;
        turrets.Remove(turret);
    }

}
