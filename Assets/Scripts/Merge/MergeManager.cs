using Finark.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MergeManager : MonoBehaviour
{

    [SerializeField] private Turret selectedTurret;


    [SerializeField] private GameObject canBuildIconPrefab;

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
                DragManager.Instance.StartDrag(DragType.Sprite, DragEnded);
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

                if (turret.Id == SelectedTurret.Id)
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
            if (GridCreator.Instance.Grid.GetValue(pos).CurrentObject != null)
            {
                Destroy(GridCreator.Instance.Grid.GetValue(pos).CurrentObject);
            }
        }

        SelectedTurret = null;
        DisableGrid();
        Destroy(obj);
    }

}
