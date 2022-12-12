using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StoreSlot : MonoBehaviour
{

    [SerializeField] private Button slotButton;
    [SerializeField] private TextMeshProUGUI slotNameText;
    [SerializeField] private TextMeshProUGUI priceText;

    public void Setup(string name, int price, Action<int> buttonAction, int index)
    {
        slotNameText.text = name;
        priceText.text = $"{price}$";
        slotButton.onClick.AddListener(() =>
        {
            buttonAction(index);
        });
    }

}