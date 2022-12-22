using Finark.Utils;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{

    [SerializeField] private Turret selectedTurret;

    [SerializeField] private GameObject canBuildIconPrefab;

    [SerializeField] private UpgradePathPicker upgradePathPicker;

    private List<GameObject> drawnCanBuildIcons = new List<GameObject>();
    private GameObject _tempBuildingButton;

    public Turret SelectedTurret
    { 
        get 
        { 
            return selectedTurret; 
        } 
        set 
        { 
            selectedTurret = value;
            if (value != null)
            {
                EnableGrid();
                DragManager.Instance.StartDrag(selectedTurret.TurretInfo.Icon, DragType.Sprite, DragEnded);
            }
        } 
    }

    #region Singleton

    private static MergeManager _instance;

    public static MergeManager Instance
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

    public void EnableGrid()
    {
        foreach (var cell in GridCreator.Instance.Grid.GridArray)
        {
            if (cell.CurrentObject == null) continue;

            if (cell.CurrentObject.TryGetComponent(out Turret turret))
            {

                if (turret.TurretInfo.Id == SelectedTurret.TurretInfo.Id)
                {
                    _tempBuildingButton = Instantiate(canBuildIconPrefab);
                    drawnCanBuildIcons.Add(_tempBuildingButton);
                    _tempBuildingButton.transform.position = cell.GetGridPosition();
                    _tempBuildingButton = null;
                }
            }
        }
    }

    public void DisableGrid()
    {
        foreach (var cell in drawnCanBuildIcons)
        {
            Destroy(cell.gameObject);
        }
        drawnCanBuildIcons.Clear();
    }

    private void DragEnded(GameObject obj)
    {
        Vector3 pos = MyUtils.GetMouseWorldPosition();
        if (GridCreator.Instance.Grid.IsInsideOfGrid(pos))
        {
            GridCell underMouse = GridCreator.Instance.Grid.GetValue(pos);
            if (underMouse.CurrentObject != null)
            {
                if (underMouse.CurrentObject.TryGetComponent(out Turret turret))
                {

                    if(turret.GetPlanesShotDown() >= turret.TurretInfo.PlaneKillsNeededToMerge && selectedTurret.GetPlanesShotDown() >= selectedTurret.TurretInfo.PlaneKillsNeededToMerge)

                    if (turret.UniqueId != selectedTurret.UniqueId)
                    {
                        if (turret.TurretInfo.Id == selectedTurret.TurretInfo.Id)
                        {
                            Merge(underMouse, turret);
                        }
                    }
                }
            }
        }

        SelectedTurret = null;
        DisableGrid();
        Destroy(obj);
    }

    private void Merge(GridCell cell, Turret upgrade)
    {

        var grid = GridCreator.Instance.Grid.GetValue(selectedTurret.transform.position);
        var selectedGameObject = grid.CurrentObject;

        selectedGameObject.gameObject.SetActive(false);

        //Activate and setup merge path chooser
        upgradePathPicker.gameObject.SetActive(true);

        upgradePathPicker.Setup(upgrade.GetPossibleMerges()[0].TurretInfo.Icon, upgrade.GetPossibleMerges()[1].TurretInfo.Icon, (int index) => {

            //Destroy not the merge target
            grid.CurrentObject = null;
            Destroy(selectedGameObject);

            //Clear the original object in the cell
            var tempObject = cell.CurrentObject;
            cell.CurrentObject = null;
            Destroy(tempObject);

            BuildingManager.Instance.Build(upgrade.GetPossibleMerges()[index].gameObject, cell.GetGridPosition()).TryGetComponent(out Turret turret);
            upgradePathPicker.gameObject.SetActive(false);
        }, 
        () => {
            selectedGameObject.gameObject.SetActive(true);
        });
    }
}
