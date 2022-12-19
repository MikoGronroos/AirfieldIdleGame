using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private int targetFrameRate = 0;

    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
