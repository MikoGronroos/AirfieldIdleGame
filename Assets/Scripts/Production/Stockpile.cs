using System.Collections.Generic;
using UnityEngine;

public class Stockpile : MonoBehaviour
{

    [SerializeField] private StockpileItemUI stockpileItemPrefab;

    [field: SerializeField] public List<StockpileItem> Stockpiles { get; set; } = new List<StockpileItem>();

    #region Singleton

    private static Stockpile _instance;

    public static Stockpile Instance
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

    public void AddToStockpile(Item item, int amount)
    {
        if (!HasStockpile(item))
        {
            Stockpiles.Add(new StockpileItem(item, amount));
        }
        else
        {
            FindStockpile(item).AmountOfItems += amount;
        }
    }
    
    public bool RemoveFromStockpile(Item item, int amount)
    {
        if (HasStockpile(item))
        {
            var stock = FindStockpile(item);

            if (stock.AmountOfItems < amount) return false;

            stock.AmountOfItems -= amount;

            return true;
        }
        return false;
    }

    private bool HasStockpile(Item item)
    {
        if (Stockpiles.Count <= 0) return false;

        bool value = false;

        foreach (var stock in Stockpiles)
        {
            if (stock.Item.Name == item.Name)
            {
                value = true;
                break;
            }
        }

        return value;
    }

    private StockpileItem FindStockpile(Item item)
    {
        StockpileItem stockItem = null;
        foreach (var stock in Stockpiles)
        {
            if (stock.Item.Name == item.Name)
            {
                stockItem = stock;
                break;
            }
        }
        return stockItem;
    }

}

[System.Serializable]
public class StockpileItem
{
    public Item Item;
    public int AmountOfItems;

    public StockpileItem(Item item, int amountOfItems)
    {
        Item = item;
        AmountOfItems = amountOfItems;
    }

}
