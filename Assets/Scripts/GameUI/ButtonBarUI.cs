using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ButtonBarUI : MonoBehaviour
{

    [SerializeField] private BarItem[] barItems;

    private void Start()
    {
        foreach (var item in barItems)
        {
            item.Setup(DisableAllPanels);
        }
    }

    private void DisableAllPanels(BarItem barItem)
    {
        foreach (var item in barItems.Where(t => t.IsActive).ToArray())
        {

            if (item == barItem) continue;

            item.ToggleGraphic(false);
        }
    }

    [Serializable]
    private class BarItem
    {
        [SerializeField] private string Name;
        [SerializeField] private GameObject GraphicTarget;
        [SerializeField] private Button InteractButton;

        public delegate void CallbackDelegate(BarItem barItem);

        public bool IsActive { get; private set; }

        public void Setup(params CallbackDelegate[] parameters)
        {
            InteractButton.onClick.AddListener(()=> {
                foreach (var item in parameters)
                {
                    item?.Invoke(this);
                }
                IsActive = !GraphicTarget.activeSelf;
                ToggleGraphic(IsActive);
            });
        }

        public void ToggleGraphic(bool value)
        {
            GraphicTarget.SetActive(value);
        }

    }
}