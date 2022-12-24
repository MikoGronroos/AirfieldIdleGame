using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{

    [SerializeField] private int startCurrency = 0;
    [SerializeField] private int currentCurrency = 0;

    public int CurrentCurrency 
    { 
        get 
        { 
            return currentCurrency;
        } 
        set {
            currentCurrency = value; 
            currencyText.text = $"{currentCurrency}$"; 
        }
    }

    [SerializeField] private TextMeshProUGUI currencyText;

    #region Singleton

    private static CurrencyManager _instance;

    public static CurrencyManager Instance
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
        CurrentCurrency = startCurrency;
    }

    public bool HasEnoughCurrency(int price)
    {
        return currentCurrency >= price;
    }

    public bool TryUseMoney(int price)
    {
        if (!HasEnoughCurrency(price)) return false;
        CurrentCurrency -= price;
        return true;
    }


}
