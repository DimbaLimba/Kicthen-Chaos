using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSwawned;
    public event EventHandler OnRecipeComplete;
    public event EventHandler OnRecipeSecces;
    public event EventHandler OnRecipeFalied;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO _recipeListSO;

    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTime;
    private float _spawnRecipeTimeMax = 4f;
    private int _spawnRecipesMax = 4;
    private int _sussecfullRecipeAmount;

    private void Awake()
    {
        Instance = this;

        _waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        _spawnRecipeTime -= Time.deltaTime;
        if (_spawnRecipeTime <= 0)
        {
            _spawnRecipeTime = _spawnRecipeTimeMax;

            if (_waitingRecipeSOList.Count < _spawnRecipesMax)
            {
                RecipeSO waitingRecipeSO = _recipeListSO.recipeSOList[UnityEngine.Random.Range(0, _recipeListSO.recipeSOList.Count)];

                OnRecipeSwawned?.Invoke(this, EventArgs.Empty);
                _waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKichenObject plateKichenObject)
    {
        for (int i = 0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];

            if (waitingRecipeSO.kicthenObjectSOList.Count == plateKichenObject.GetKicthenObjectSOList().Count)
            {
                bool plateContentMatchesRecipe = true;

                foreach (KicthenObjectSO recipeKicthenObjectSO in waitingRecipeSO.kicthenObjectSOList)
                {
                    bool ingridiendFound = false;

                    foreach (KicthenObjectSO plateKicthenObjectSO in plateKichenObject.GetKicthenObjectSOList())
                    {
                        if (plateKicthenObjectSO == recipeKicthenObjectSO)
                        {
                            ingridiendFound = true;
                            break;
                        }
                    }
                    if (!ingridiendFound)
                    {
                        plateContentMatchesRecipe = false;
                    }
                }
                if (plateContentMatchesRecipe)
                {
                    _sussecfullRecipeAmount++;

                    _waitingRecipeSOList.RemoveAt(i);

                    OnRecipeComplete?.Invoke(this, EventArgs.Empty);
                    OnRecipeSecces?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
            OnRecipeFalied?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return _waitingRecipeSOList;
    }

    public int GetSussecfullRecipeAmount()
    {
        return _sussecfullRecipeAmount;
    }
}
