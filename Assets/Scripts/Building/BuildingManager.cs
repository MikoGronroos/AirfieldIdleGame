using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{

    #region Singleton

    private static BuildingManager _instance;

    public static BuildingManager Instance { get { return _instance; } }

    #endregion

    [SerializeField] private GameObject canBuildIconPrefab;

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
                cell.CurrentObject = Instantiate(canBuildIconPrefab);
                cell.CurrentObject.GetComponent<BuildingButton>().Setup(() =>
                {
                    DisableBuildingGrid();
                });
            }
        }
    }

    public void DisableBuildingGrid()
    {
        foreach (var cell in GridCreator.Instance.Grid.GridArray)
        {
            Destroy(cell.CurrentObject);
        }
    }
}
