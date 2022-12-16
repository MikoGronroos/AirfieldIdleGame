using UnityEngine;
using DG.Tweening;

public class UIEffectManager : MonoBehaviour
{

    [Header("Kill confirmed UI")]
    [SerializeField] private ObjectPool killConfirmedPool;
    [Range(0,1)]
    [SerializeField] private float killConfirmedFadeValue = 1;
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
                uiEffect.killNumberText.DOFade(killConfirmedFadeValue, killConfirmedTime);
                uiEffect.transform.DOMoveY(pos.y + yOffset, killConfirmedTime).OnComplete(() => {
                    uiEffect.killNumberText.alpha = 1;
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
