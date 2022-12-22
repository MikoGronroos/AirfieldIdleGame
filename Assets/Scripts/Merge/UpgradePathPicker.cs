using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePathPicker : MonoBehaviour
{

    [SerializeField] private Button firstOptionButton;
    [SerializeField] private Button secondOptionButton;

    [SerializeField] private CloseButton closeButton;

    private Action<int> mergeAction;

    private void Awake()
    {
        InitializeButtons();
    }

    public void Setup(Sprite icon1, Sprite icon2, Action<int> action, Action closeButtonCallback)
    {
        firstOptionButton.image.sprite = icon1;
        secondOptionButton.image.sprite = icon2;
        mergeAction = action;
        closeButton.SetCloseAction(closeButtonCallback);
    }

    private void InitializeButtons()
    {
        firstOptionButton.onClick.AddListener(() => {
            mergeAction(0);
        });
        secondOptionButton.onClick.AddListener(() => {
            mergeAction(1);
        });
    }

}
