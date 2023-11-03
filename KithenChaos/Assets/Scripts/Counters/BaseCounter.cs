using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKichenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }

    [SerializeField] private Transform _counterTopPoint;

    private KichenObject _kichenObject;

    public virtual void Interact(Player player)
    {
       
    }

    public virtual void InteractAlternate(Player player)
    {

    }

    public Transform GetKitchenObjectFolowTransform()
    {
        return _counterTopPoint;
    }

    public void SetKichenObject(KichenObject kichenObject)
    {
        _kichenObject = kichenObject;

        if (kichenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KichenObject GetKichenObject()
    {
        return _kichenObject;
    }

    public void ClearKichenObject()
    {
        _kichenObject = null;
    }

    public bool HasKichenObject()
    {
        return _kichenObject != null;
    }
}
