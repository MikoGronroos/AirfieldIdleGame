using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretManager : MonoBehaviour
{

    [SerializeField] private List<Turret> turrets = new List<Turret>();

    private void Awake()
    {
        turrets = FindObjectsOfType<Turret>().ToList();
    }

    private void Update()
    {
        foreach (var turret in turrets)
        {
            turret.UpdateTick();
        }
    }
}
