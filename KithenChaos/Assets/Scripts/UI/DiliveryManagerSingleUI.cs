using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeTextName;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);    
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        _recipeTextName.text = recipeSO.recipeName;

        foreach (Transform child in _iconContainer)
        {
            if (child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KicthenObjectSO kicthenObjectSO in recipeSO.kicthenObjectSOList)
        {
            Transform iconTransform = Instantiate(_iconTemplate, _iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kicthenObjectSO.Sprite;
        }
    }
}
