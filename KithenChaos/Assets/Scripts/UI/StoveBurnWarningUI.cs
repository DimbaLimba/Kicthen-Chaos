using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;

    private void Start()
    {
        _stoveCounter.OnProgressChanged += _stoveCounter_OnProgressChanged;
        Hide();
    }

    private void _stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgresAmount = .5f;
        bool show = _stoveCounter.IsFried() && e.progressNormalized > burnShowProgresAmount;

        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
