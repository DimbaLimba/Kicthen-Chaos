using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrahed;

   new public static void ResetStaticData()
    {
        OnAnyObjectTrahed = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKichenObject())
        {
            player.GetKichenObject().DestroySelf();

            OnAnyObjectTrahed(this, EventArgs.Empty);
        }
    }
}
