using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StoreSlot : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI slotNameText;
    [SerializeField] private TextMeshProUGUI priceText;

    private Action<int> _buttonAction;
    private int _index = 0;

    public void Setup(string name, int price, Action<int> buttonAction, int index)
    {
        slotNameText.text = name;
        priceText.text = $"{price}$";
        _index = index;
        _buttonAction = buttonAction;
    }

    public void StartDragging()
    {
        _buttonAction?.Invoke(_index);
    }

}