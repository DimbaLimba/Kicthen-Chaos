using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _recipeTempate;

    private void Awake()
    {
        _recipeTempate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSwawned += DeliveryManager_OnRecipeSwawned;
        DeliveryManager.Instance.OnRecipeComplete += DeliveryManager_OnRecipeComplete;

        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeComplete(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSwawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in _container)
        {
            if (child == _recipeTempate) continue;   
                Destroy(child.gameObject);       
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(_recipeTempate, _container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DiliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
