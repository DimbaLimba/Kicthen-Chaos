using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningFishingUI : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;

    private const string IS_FISHING = "IsFlashing";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();   
    }

    private void Start()
    {
        _stoveCounter.OnProgressChanged += _stoveCounter_OnProgressChanged;
        _animator.SetBool(IS_FISHING, false);

    }

    private void _stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgresAmount = .5f;
        bool show = _stoveCounter.IsFried() && e.progressNormalized > burnShowProgresAmount;

        _animator.SetBool(IS_FISHING, show);

       
    }
}
