using UnityEngine;

[CreateAssetMenu(menuName = "Store Item")]
public class StoreItem : ScriptableObject
{
    public string ItemName;
    public GameObject Prefab;
    public int Price;
    private int BuyAmount = 0;

    public bool HasBeenBought()
    {
        return BuyAmount > 0;
    }

}