using System;
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

        for (int i = 0; i < productionLines.Count; i++)
        {
            int tempIndex = i;
            var line = productionLines[i];
            ProductionItemUI ui = Instantiate(productionLinePrefab, productionLineParent);
            _productionItemUIs.Add(ui);
            ui.Setup(line.BaseProductionAmount, line.Item.name);
            ui.SetupButton(() => {
                UpgradeProductionLine(tempIndex);
            });
        }

    }

    private void UpgradeProductionLine(int index)
    {
        productionLines[index].Upgrade(() => {
            _productionItemUIs[index].Setup(productionLines[index].BaseProductionAmount, productionLines[index].Item.name);
        });
    }
}

[Serializable]
public class ProductionItem
{
    public Item Item;
    public int BaseProductionAmount;
    public int ProductionLineLevel;

    private float _currentTime;

    public void Tick()
    {
        _currentTime = Mathf.Clamp(_currentTime += 1 * Time.deltaTime, 0, 1);

        if (_currentTime >= 1)
        {
            Stockpile.Instance.AddToStockpile(Item, BaseProductionAmount);
            _currentTime = 0;
        }
    }

    public void Upgrade(Action onUpgraded)
    {
        ProductionLineLevel += 1;
        BaseProductionAmount += 1;
        onUpgraded?.Invoke();
    }

}
