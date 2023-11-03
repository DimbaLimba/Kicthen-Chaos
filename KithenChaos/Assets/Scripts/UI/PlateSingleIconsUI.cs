using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateSingleIconsUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetKichenObjectSO(KicthenObjectSO kicthenObjectSO)
    {
        _image.sprite = kicthenObjectSO.Sprite;
    }
}
