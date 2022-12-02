using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{

    [SerializeField] private Plane planePrefab;

    [SerializeField] private int minPlanes;
    [SerializeField] private int maxPlanes;

    [SerializeField] private List<Plane> planes = new List<Plane>();
    [SerializeField] private List<GameObject> planeSpawnPositions = new List<GameObject>();

    [SerializeField] private ObjectPool _pool;

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

        _planeAmount = Random.Range(minPlanes, maxPlanes);

        for (int i = 0; i < _planeAmount; i++)
        {

            Plane plane = _pool.Get() as Plane;
            plane.transform.position = planeSpawnPositions[Random.Range(0, planeSpawnPositions.Count - 1)].transform.position;
            planes.Add(plane);
        }
    }

    public void RemovePlane(Plane plane)
    {

        _pool.Release(plane);
        planes.Remove(plane);

        if (planes.Count <= 0) 
        {
            SpawnPlanes();
        }
    }

}
