using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIButton : MonoBehaviour
{

    private Image _image;
    private List<Transform> _elements;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _elements = GetComponentsInChildren<Transform>().ToList();
    }

    public void Show()
    {
        _image.enabled = true;

        foreach (var element in _elements)
        {
            if (element.Equals(transform))
                continue;
            element.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        _image.enabled = false;

        foreach (var element in _elements)
        {
            if (element.Equals(transform))
                continue;
            element.gameObject.SetActive(false);
        }
    }
}