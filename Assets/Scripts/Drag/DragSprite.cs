using Finark.Utils;
using System;
using System.Collections;
using UnityEngine;

public class DragSprite : MonoBehaviour
{

    private SpriteRenderer _renderer;
    private Action<GameObject> _onDragEnded;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Setup(Sprite icon, Vector3 pos, Action<GameObject> onDragEnded)
    {
        _onDragEnded = onDragEnded;
        if (icon != null)
        {
            _renderer.sprite = icon;
        }
        transform.position = pos;
        StartCoroutine(SyntheticDrag());
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseUp()
    {
        _onDragEnded(gameObject);
    }

    private void OnMouseDrag()
    {
        transform.position = MyUtils.GetMouseWorldPosition();
    }


    IEnumerator SyntheticDrag()
    {
        OnMouseDown();
        yield return null;

        while (Input.GetMouseButton(0))
        {
            OnMouseDrag();
            yield return null;
        }
        OnMouseUp();
    }

}
