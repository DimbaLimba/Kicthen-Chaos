using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut; 

   new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;


    [SerializeField] private CuttingRecingSO[] _cutKicthenObjectSOArray;

    private int _cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKichenObject())
        {
            if (player.HasKichenObject())
            {
                if (HasRecipeWithInput(player.GetKichenObject().GetKicthenObjectSO()))
                {
                    player.GetKichenObject().SetKichenObjectParent(this);
                    _cuttingProgress = 0;

                    CuttingRecingSO cuttingRecingSO = GetCuttingRecipeSOWithInput(GetKichenObject().GetKicthenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)_cuttingProgress / cuttingRecingSO.cuttingProgresMax
                    });

                }
            }
        }
        else
        {
            if (player.HasKichenObject())
            {
                if (player.GetKichenObject().TryGetPlate(out PlateKichenObject plateKichenObject))
                {
                    if (plateKichenObject.TryAddIngrediend(GetKichenObject().GetKicthenObjectSO()))
                    {
                        GetKichenObject().DestroySelf();
                    }
                }
            }
            else
            {
                GetKichenObject().SetKichenObjectParent(player);
            }

        }
        
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKichenObject() && HasRecipeWithInput(GetKichenObject().GetKicthenObjectSO()))
        {
            _cuttingProgress++;
            CuttingRecingSO cuttingRecingSO = GetCuttingRecipeSOWithInput(GetKichenObject().GetKicthenObjectSO());

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)_cuttingProgress / cuttingRecingSO.cuttingProgresMax
            });

            if (_cuttingProgress >= cuttingRecingSO.cuttingProgresMax)
            {
            KicthenObjectSO outputKicthenObjectSO = GetOutputForInput(GetKichenObject().GetKicthenObjectSO());

            GetKichenObject().DestroySelf();

            KichenObject.SpawnKichenObject(outputKicthenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KicthenObjectSO inputKicthenObjectSO)
    {
        foreach (CuttingRecingSO cuttingRecingSO in _cutKicthenObjectSOArray)
        {
            if (cuttingRecingSO.input == inputKicthenObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    private KicthenObjectSO GetOutputForInput(KicthenObjectSO inputKicthenObjectSO)
    {
        CuttingRecingSO cuttingRecingSO = GetCuttingRecipeSOWithInput(inputKicthenObjectSO);

        if (cuttingRecingSO != null)
        {
            return cuttingRecingSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecingSO GetCuttingRecipeSOWithInput(KicthenObjectSO inputKicthenObjectSO)
    {
        foreach (CuttingRecingSO cuttingRecingSO in _cutKicthenObjectSOArray)
        {
            if (cuttingRecingSO.input == inputKicthenObjectSO)
            {
                return cuttingRecingSO;
            }
        }
        return null;
    }
}
