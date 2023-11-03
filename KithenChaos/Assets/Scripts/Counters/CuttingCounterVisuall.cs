using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisuall : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCouter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _cuttingCouter.OnCut += _containerCounter_OnPlayerGrabdedObject;
    }

    private void _containerCounter_OnPlayerGrabdedObject(object sender, System.EventArgs e)
    {
        _animator.SetTrigger("Cut");
    }
}
