using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{

    [SerializeField] private Button storeButton;

    public void Setup(Action method)
    {
        storeButton.onClick.AddListener(() => {
            method();
        });
    }
}
