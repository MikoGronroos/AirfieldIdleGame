using UnityEngine;

public class Airfield : MonoBehaviour
{

    [field: SerializeField] public int PowerScore { get; set; } = 10;

    #region Singleton

    private static Airfield _instance;

    public static Airfield Instance
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

}
