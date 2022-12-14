using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    #region Singleton

    private static BuildingManager _instance;

    public static BuildingManager Instance { get { return _instance; } }

    #endregion

    [SerializeField] private GameObject canBuildIconPrefab;

    private List<GameObject> drawnCanBuildIcons = new List<GameObject>();
    private GameObject _tempBuildingButton;

    private void Awake()
    {
        _instance = this;
    }

    public void EnableBuildingGrid()
    {
        foreach (var cell in GridCreator.Instance.Grid.GridArray)
        {
            if (cell.IsEmpty())
            {
                _tempBuildingButton = Instantiate(canBuildIconPrefab);
                drawnCanBuildIcons.Add(_tempBuildingButton);
                cell.CurrentObject = _tempBuildingButton;
                _tempBuildingButton = null;
            }
        }
    }

    public void DisableBuildingGrid()
    {
        foreach (var cell in drawnCanBuildIcons)
        {
            Destroy(cell.gameObject);
        }
        drawnCanBuildIcons.Clear();
    }

    public GameObject Build(GameObject prefab, Vector3 pos)
    {
        DisableBuildingGrid();
        var go = Instantiate(prefab);
        if (go.TryGetComponent(out Turret turret))
        {
            TurretManager.Instance.AddTurret(turret);
        }
        GridCreator.Instance.Grid.GetValue(pos).CurrentObject = go;
        return go;
    }

}
