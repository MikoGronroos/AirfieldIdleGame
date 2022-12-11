using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StoreSlot : MonoBehaviour
{

    [SerializeField] private Button slotButton;
    [SerializeField] private TextMeshProUGUI slotNameText;

    public void Setup(string name, Action<int> buttonAction, int index)
    {
        slotNameText.text = name;
        slotButton.onClick.AddListener(() =>
        {
            buttonAction(index);
        });
    }

}