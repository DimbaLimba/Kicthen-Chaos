using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKichenObject _plateKichenObject;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        _plateKichenObject.OnIngrediendAded += _plateKichenObject_OnIngrediendAded;
    }

    private void _plateKichenObject_OnIngrediendAded(object sender, PlateKichenObject.OnIngredidendAdedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KicthenObjectSO kicthenObjectSO in _plateKichenObject.GetKicthenObjectSOList())
        {
            Transform IconTemplate = Instantiate(_iconTemplate, transform);
            IconTemplate.gameObject.SetActive(true);
            IconTemplate.GetComponent<PlateSingleIconsUI>().SetKichenObjectSO(kicthenObjectSO);
        }
    }
}
