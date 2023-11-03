using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KichenObjectSO_GameObject
    {
       public KicthenObjectSO kicthenObjectSO;
       public GameObject gameObject;
    }

    [SerializeField] private PlateKichenObject _plateKichenObject;
    [SerializeField] private List<KichenObjectSO_GameObject> _kichenObjectSO_GameObjectList;

    private void Start()
    {
        _plateKichenObject.OnIngrediendAded += _plateKichenObject_OnIngrediendAded;

        foreach (KichenObjectSO_GameObject kichenObjectSO_GameObject in _kichenObjectSO_GameObjectList)
        {
            kichenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }

    private void _plateKichenObject_OnIngrediendAded(object sender, PlateKichenObject.OnIngredidendAdedEventArgs e)
    {
        foreach (KichenObjectSO_GameObject kichenObjectSO_GameObject in _kichenObjectSO_GameObjectList)
        {
            if (kichenObjectSO_GameObject.kicthenObjectSO == e.kicthenObjectSO)
            {
                kichenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
