using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StoreSlot : MonoBehaviour
{

    [SerializeField] private Button slotButton;
    [SerializeField] private TextMeshProUGUI slotNameText;

    public void Setup(string name, Action buttonAction)
    {
        slotNameText.text = name;
        slotButton.onClick.AddListener(() =>
        {
            buttonAction();
        });
    }

}

public class StoreItem : ScriptableObject
{
    public string itemName;
    public GameObject prefab;
    private int buyAmount = 0;

    public bool HasBeenBought()
    {
        return buyAmount > 0;
    }

}