using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{

    [SerializeField] private int currency = 0;

    public int Currency 
    { 
        get 
        { 
            return currency;
        } 
        set {
            currency = value; 
            currencyText.text = $"{currency}$"; 
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

    public bool HasEnoughCurrency(int price)
    {
        return currency >= price;
    }

}
