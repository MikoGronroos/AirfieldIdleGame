using Finark.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{

    [SerializeField] private List<DragObject> dragObjects = new List<DragObject>();

    #region Singleton

    private static DragManager _instance;

    public static DragManager Instance { get { return _instance; } }

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    public void StartDrag(DragType type, Action<GameObject> onDragEnded)
    {
        DragObject temp = FindDragObject(type);
        switch (type)
        {
            case DragType.Sprite:
                Instantiate(temp.Prefab).GetComponent<DragSprite>().Setup(null, MyUtils.GetMouseWorldPosition(), onDragEnded);
                break;
            default:
                break;
        }
    }

    private DragObject FindDragObject(DragType type)
    {
        DragObject temp = null;
        foreach (var item in dragObjects)
        {
            if (item.DragType == type)
            {
                temp = item;
                break;
            }
        }
        return temp;
    }

}

[Serializable]
public class DragObject
{
    public GameObject Prefab;
    public DragType DragType;
}

public enum DragType
{
    Sprite
}
