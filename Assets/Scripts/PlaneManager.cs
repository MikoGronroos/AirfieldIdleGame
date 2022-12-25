using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{

    [SerializeField] private Plane planePrefab;

    [SerializeField] private float planePowerScoreMultiplier;

    [SerializeField] private List<Plane> planes = new List<Plane>();
    [SerializeField] private List<GameObject> planeSpawnPositions = new List<GameObject>();

    [SerializeField] private ObjectPool pool;

    private int _planeAmount = 0;

    #region Singleton

    private static PlaneManager _instance;

    public static PlaneManager Instance
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

    private void Start()
    {
        SpawnPlanes();
    }

    private void SpawnPlanes()
    {
        if (planes.Count > 0) return;

        _planeAmount = (int)(Airfield.Instance.PowerScore * planePowerScoreMultiplier);
        Debug.Log(_planeAmount);

        for (int i = 0; i < _planeAmount; i++)
        {

            Plane plane = pool.Get() as Plane;
            plane.transform.position = planeSpawnPositions[Random.Range(0, planeSpawnPositions.Count - 1)].transform.position;
            planes.Add(plane);
        }
    }

    public void RemovePlane(Plane plane)
    {

        pool.Release(plane);
        planes.Remove(plane);

        if (planes.Count <= 0) 
        {
            SpawnPlanes();
        }
    }

}
