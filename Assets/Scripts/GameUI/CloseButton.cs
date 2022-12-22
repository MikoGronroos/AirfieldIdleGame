using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{

    [SerializeField] private UnityEvent closeEvent;

    private Button _closeButton;
    private Action _closeAction;

    private void Awake()
    {
        _closeButton = GetComponent<Button>();
        _closeButton.onClick.AddListener(() => {
            closeEvent?.Invoke();
            _closeAction?.Invoke();
        });
    }

    public void SetCloseAction(Action closeAction)
    {
        _closeAction = closeAction;
    }

}
