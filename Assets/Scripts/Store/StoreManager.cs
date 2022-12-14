using System.Collections.Generic;
using UnityEngine;
using Finark.Utils;
using System.Linq;

public class StoreManager : MonoBehaviour
{

    [SerializeField] private List<StoreItem> storeItems = new List<StoreItem>();

    [SerializeField] private StoreSlot storeSlotPrefab;
    [SerializeField] private Transform storeSlotParent;

    [SerializeField] private DragSprite dragSpritePrefab;

    private List<StoreSlot> _drawnStoreItems = new List<StoreSlot>();
    private StoreItem _currentlySelectedStoreItem;

    #region Singleton

    private static StoreManager _instance;

    public static StoreManager Instance { get { return _instance; } }

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        UpdateStore();
    }

    private void UpdateStore()
    {
        if (_drawnStoreItems.Count > 0)
        {
            for (int i = _drawnStoreItems.Count - 1; i >= 0; i--)
            {
                var slot = _drawnStoreItems[i].gameObject;
                _drawnStoreItems.RemoveAt(i);
                Destroy(slot);
            }
        }

        storeItems = storeItems.OrderBy(t => t.Price).ToList();

        for (int i = 0; i < storeItems.Count; i++)
        {
            var line = storeItems[i];
            StoreSlot ui = Instantiate(storeSlotPrefab, storeSlotParent);
            _drawnStoreItems.Add(ui);
            ui.Setup(storeItems[i].TurretInfo.turretName, storeItems[i].Price, OnStoreItemSelected, i);
        }
    }

    private void OnStoreItemSelected(int index)
    {
        _currentlySelectedStoreItem = storeItems[index];
        BuildingManager.Instance.EnableBuildingGrid();
        DragManager.Instance.StartDrag(_currentlySelectedStoreItem.TurretInfo.Icon, DragType.Sprite, DragEnded);
    }

    private void DragEnded(GameObject obj)
    {
        if (GridCreator.Instance.Grid.IsInsideOfGrid(obj.transform.position))
        {

            if (CurrencyManager.Instance.TryUseMoney(_currentlySelectedStoreItem.Price))
            {
                BuildingManager.Instance.Build(_currentlySelectedStoreItem.TurretInfo.Prefab, obj.transform.position);
            }
        }
        _currentlySelectedStoreItem = null;
        BuildingManager.Instance.DisableBuildingGrid();
        Destroy(obj);
    }

    public void AddStoreItem(StoreItem item)
    {
        if (!storeItems.Contains(item))
        {
            storeItems.Add(item);
        }
        UpdateStore();
    }
}
