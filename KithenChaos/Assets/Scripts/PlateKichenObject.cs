using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKichenObject : KichenObject
{
    public static event EventHandler OnIngrediendSound;
    public event EventHandler<OnIngredidendAdedEventArgs> OnIngrediendAded;
    public class OnIngredidendAdedEventArgs : EventArgs
    {
        public KicthenObjectSO kicthenObjectSO;
    }

    [SerializeField] private List<KicthenObjectSO> _validKicthenObjectSO;

    private List<KicthenObjectSO> _kicthenObjectSOList;

    private void Awake()
    {
        _kicthenObjectSOList = new List<KicthenObjectSO>();
    }

    public bool TryAddIngrediend(KicthenObjectSO kichenObject)
    {
        if (!_validKicthenObjectSO.Contains(kichenObject))
        {
            return false;
        }
        if (_kicthenObjectSOList.Contains(kichenObject))
        {
            return false;
        }
        else 
        {
            _kicthenObjectSOList.Add(kichenObject);

            OnIngrediendSound?.Invoke(this, EventArgs.Empty);

            OnIngrediendAded?.Invoke(this, new OnIngredidendAdedEventArgs
            {
                kicthenObjectSO = kichenObject
            });
            return true;
        }
    }

    public List<KicthenObjectSO> GetKicthenObjectSOList()
    {
        return _kicthenObjectSOList;
    }
}
