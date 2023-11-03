using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabdedObject;

    [SerializeField] private KicthenObjectSO _kicthenObjectSO;

    public override void Interact(Player player)
    {
            if (!player.HasKichenObject())
            {
            KichenObject.SpawnKichenObject(_kicthenObjectSO, player);
            }
    }
}
