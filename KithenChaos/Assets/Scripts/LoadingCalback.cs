using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCalback : MonoBehaviour
{
    private bool _isFirstUpdate = true;

    private void Update()
    {
        if (_isFirstUpdate)
        {
            _isFirstUpdate = false;

            Loader.LoadingColback();
        }
    }
}
