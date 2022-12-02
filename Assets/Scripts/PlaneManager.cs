using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{

    [SerializeField] private GameObject planePrefab;

    [SerializeField] private int minPlanes;
    [SerializeField] private int maxPlanes;

    [SerializeField] private List<GameObject> planes = new List<GameObject>();
    [SerializeField] private List<GameObject> planeSpawnPositions = new List<GameObject>();

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
            GameObject plane = Instantiate(planePrefab);
            plane.transform.position = planeSpawnPositions[Random.Range(0, planeSpawnPositions.Count - 1)].transform.position;
            planes.Add(plane);
        }
    }

    public void RemovePlane(GameObject plane)
    {

        var planeCopy = plane;
        planes.Remove(plane);
        Destroy(planeCopy);

        if (planes.Count <= 0) 
        {
            SpawnPlanes();
        }
    }

}
