using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{

    [SerializeField] private List<string> storeItems = new List<string>();

    [SerializeField] private StoreSlot storeSlotPrefab;
    [SerializeField] private Transform sotreSlotParent;

    private List<StoreSlot> drawnStoreItems = new List<StoreSlot>();

    private void Start()
    {
        UpdateStore();
    }

    private void UpdateStore()
    {
        if (drawnStoreItems.Count > 0)
        {
            for (int i = drawnStoreItems.Count - 1; i >= 0; i--)
            {
                var slot = drawnStoreItems[i].gameObject;
                drawnStoreItems.RemoveAt(i);
                Destroy(slot);
            }
        }

        for (int i = 0; i < storeItems.Count; i++)
        {
            int tempIndex = i;
            var line = storeItems[i];
            StoreSlot ui = Instantiate(storeSlotPrefab, sotreSlotParent);
            drawnStoreItems.Add(ui);
            ui.Setup(storeItems[i]);
        }
    }
}
