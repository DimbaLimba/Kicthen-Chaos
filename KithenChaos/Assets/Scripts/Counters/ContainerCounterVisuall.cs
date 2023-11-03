using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisuall : MonoBehaviour
{
    [SerializeField] private ContainerCounter _containerCounter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _containerCounter.OnPlayerGrabdedObject += _containerCounter_OnPlayerGrabdedObject;
    }

    private void _containerCounter_OnPlayerGrabdedObject(object sender, System.EventArgs e)
    {
        _animator.SetTrigger("OpenClose");
    }
}
