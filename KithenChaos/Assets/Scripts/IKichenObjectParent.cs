using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKichenObjectParent
{
    public Transform GetKitchenObjectFolowTransform();

    public void SetKichenObject(KichenObject kichenObject);

    public KichenObject GetKichenObject();

    public void ClearKichenObject();

    public bool HasKichenObject();
}
