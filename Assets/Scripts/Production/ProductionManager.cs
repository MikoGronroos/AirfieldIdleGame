using System.Collections.Generic;
using UnityEngine;

public class ProductionManager : MonoBehaviour
{

    [SerializeField] private Transform productionLineParent;
    [SerializeField] private ProductionItemUI productionLinePrefab;

    [SerializeField] private List<ProductionItem> productionLines = new List<ProductionItem>();

    private List<ProductionItemUI> _productionItemUIs = new List<ProductionItemUI>();

    #region Singleton

    private static ProductionManager _instance;

    public static ProductionManager Instance
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
        DrawProduction();
    }

    private void Update()
    {

        foreach (var prod in productionLines)
        {
            prod.Tick();
        }

    }

    private void DrawProduction()
    {

        if (_productionItemUIs.Count > 0)
        {
            for (int i = _productionItemUIs.Count - 1; i >= 0; i--)
            {
                var prod = _productionItemUIs[i].gameObject;
                _productionItemUIs.RemoveAt(i);
                Destroy(prod);
            }
        }

        foreach (var line in productionLines)
        {
            ProductionItemUI ui = Instantiate(productionLinePrefab, productionLineParent);
            ui.Setup(line.BaseProductionAmount, line.ProductionTime, line.Item.name);
        }

    }

}

[System.Serializable]
public class ProductionItem
{
    public Item Item;
    public int BaseProductionAmount;
    public int ProductionLineLevel;

    public float ProductionTime;

    private float _currentTime;

    public void Tick()
    {
        _currentTime = Mathf.Clamp(_currentTime += 1 * Time.deltaTime, 0, ProductionTime);

        if (_currentTime >= 1)
        {
            Stockpile.Instance.AddToStockpile(Item, BaseProductionAmount);
            _currentTime = 0;
        }
    }

}
