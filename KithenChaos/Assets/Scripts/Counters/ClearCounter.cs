using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KicthenObjectSO _kicthenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKichenObject())
        {
            if (player.HasKichenObject())
            {
                player.GetKichenObject().SetKichenObjectParent(this);
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
                else
                {
                    if(GetKichenObject().TryGetPlate(out plateKichenObject))
                    {
                        if (plateKichenObject.TryAddIngrediend(player.GetKichenObject().GetKicthenObjectSO()))
                        {
                            player.GetKichenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
            GetKichenObject().SetKichenObjectParent(player);
            }
        }
    }
}
