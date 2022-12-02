using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ProductionItemUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI amountProducedText;
    [SerializeField] private TextMeshProUGUI productionItemName;
    [SerializeField] private Button upgradeButton;

    public void Setup(int amount, string name)
    {
        amountProducedText.text = amount.ToString() + "/Sec";
        productionItemName.text = name;
    }

    public void SetupButton(Action onUpgrade)
    {
        upgradeButton.onClick.AddListener(() => {
            onUpgrade?.Invoke();
        });
    }

}