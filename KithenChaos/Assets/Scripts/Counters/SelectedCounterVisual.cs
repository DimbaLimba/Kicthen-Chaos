using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] _visualGameObjectArray;
    [SerializeField] private BaseCounter _baseCounter;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanger += Player_OnSelectedCounterChanger;   
    }

    private void Player_OnSelectedCounterChanger(object sender, Player.OnSelectedCounterEventArgs e)
    {
        if (e.selecterCounter == _baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in _visualGameObjectArray) 
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in _visualGameObjectArray)
        {
        visualGameObject.SetActive(false);
        }
    }
}
