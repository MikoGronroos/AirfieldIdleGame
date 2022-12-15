using UnityEngine;

[CreateAssetMenu(menuName = "Store Item")]
public class StoreItem : ScriptableObject
{

    public GeneralTurretInfo TurretInfo;
    public int Price;
    private int BuyAmount = 0;

    public bool HasBeenBought()
    {
        return BuyAmount > 0;
    }

}