using UnityEngine;
using DG.Tweening;

public class UIEffectManager : MonoBehaviour
{

    [Header("Kill confirmed UI")]
    [SerializeField] private ObjectPool killConfirmedPool;
    [SerializeField] private float killConfirmedTime;
    [SerializeField] private float yOffset;

    #region Singleton

    private static UIEffectManager _instance;

    public static UIEffectManager Instance { get { return _instance; } }

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    public void StartUIEffect(UIEffect effect, Vector3 pos)
    {
        switch (effect)
        {
            case UIEffect.KillConfirmed:
                var uiEffect = killConfirmedPool.Get() as KillConfirmed;
                uiEffect.transform.position = pos;
                uiEffect.killNumberText.DOFade(100, killConfirmedTime);
                uiEffect.transform.DOMoveY(pos.y + yOffset, killConfirmedTime).OnComplete(() => {
                    killConfirmedPool.Release(uiEffect);
                });
                break;
            default:
                break;
        }
    }

}

public enum UIEffect
{
    KillConfirmed
}
