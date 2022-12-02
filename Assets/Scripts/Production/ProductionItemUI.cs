using UnityEngine;
using TMPro;

public class ProductionItemUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI amountProducedText;
    [SerializeField] private TextMeshProUGUI productionTimeText;
    [SerializeField] private TextMeshProUGUI productionItemName;

    public void Setup(int amount, float time, string name)
    {
        amountProducedText.text = amount.ToString();
        productionTimeText.text = time.ToString();
        productionItemName.text = name;
    }
}