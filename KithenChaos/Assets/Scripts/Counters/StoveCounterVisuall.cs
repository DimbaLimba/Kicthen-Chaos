using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisuall : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveOnGameObject;
    [SerializeField] private GameObject _particlesObject;

    private void Start()
    {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;
    }

    private void _stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisuall = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        _stoveOnGameObject.SetActive(showVisuall);
        _particlesObject.SetActive(showVisuall);
    }
}
