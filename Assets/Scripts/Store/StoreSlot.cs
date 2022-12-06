using UnityEngine;
using TMPro;

public class StoreSlot : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI slotNameText;

    public void Setup(string name)
    {
        slotNameText.text = name;
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