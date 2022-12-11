using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    #region Singleton

    private static BuildingManager _instance;

    public static BuildingManager Instance { get { return _instance; } }

    #endregion

    [SerializeField] private BuildingButton canBuildIconPrefab;

    private List<BuildingButton> drawnCanBuildIcons = new List<BuildingButton>();
    private BuildingButton _tempBuildingButton;

    private void Awake()
    {
        _instance = this;
    }

    public void EnableBuildingGrid(StoreItem item)
    {
        foreach (var cell in GridCreator.Instance.Grid.GridArray)
        {
            if (cell.IsEmpty())
            {
                _tempBuildingButton = Instantiate(canBuildIconPrefab);
                drawnCanBuildIcons.Add(_tempBuildingButton);
                _tempBuildingButton.Setup(() =>
                {
                    var tempItem = item;
                    DisableBuildingGrid();
                    var tempCell = cell;
                    tempCell.CurrentObject = Build(tempItem); 
                });
                cell.CurrentObject = _tempBuildingButton.gameObject;
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

    private GameObject Build(StoreItem item)
    {
        return Instantiate(item.Prefab);
    }

}
