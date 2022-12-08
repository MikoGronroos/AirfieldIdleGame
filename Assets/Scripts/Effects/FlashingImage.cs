using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingImage : MonoBehaviour
{

    [SerializeField] private float flashingSpeed;
    [SerializeField] private int minFlashAlpha;
    [SerializeField] private int maxFlashAlpha;

    private Image _image;
    [SerializeField] private float _currentTime = 0;
    [SerializeField] private bool _goingForward = true;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (_goingForward)
        {
            Debug.Log("Hei1");
            if (_currentTime >= 1)
            {
                _goingForward = false;
                flashingSpeed *= -1;
            }
        }
        else
        {
            Debug.Log("Hei2");
            if (_currentTime <= 0) 
            {
                flashingSpeed *= -1;
                _goingForward = true;
            }
        }
        _currentTime = Mathf.Clamp(_currentTime + flashingSpeed * Time.deltaTime, 0, 1);
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, Mathf.Lerp(minFlashAlpha / 255, maxFlashAlpha / 255, _currentTime));
    }
}
