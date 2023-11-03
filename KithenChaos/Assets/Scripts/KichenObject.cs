using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KichenObject : MonoBehaviour
{
    [SerializeField] private KicthenObjectSO _kicthenObjectSO;

    private IKichenObjectParent _kichenObjectParent;

    public KicthenObjectSO GetKicthenObjectSO()
    {
        return _kicthenObjectSO;
    }

    public void SetKichenObjectParent(IKichenObjectParent kichenObjectParent)
    {
        if (_kichenObjectParent != null)
        {
            _kichenObjectParent.ClearKichenObject();
        }

        _kichenObjectParent = kichenObjectParent;

        kichenObjectParent.SetKichenObject(this);

        transform.parent = kichenObjectParent.GetKitchenObjectFolowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKichenObjectParent GetKichenObjectParent()
    {
        return _kichenObjectParent;
    }

    public void DestroySelf()
    {
        _kichenObjectParent.ClearKichenObject();
        Destroy(gameObject);
    }

    public static KichenObject SpawnKichenObject(KicthenObjectSO kicthenObjectSO, IKichenObjectParent kichenObjectParent)
    {
        Transform KicthenObjectTransform = Instantiate(kicthenObjectSO.Prefab); 

        KichenObject kichenObject = KicthenObjectTransform.GetComponent<KichenObject>();

        kichenObject.SetKichenObjectParent(kichenObjectParent);

        return kichenObject;
    }

    public bool TryGetPlate(out PlateKichenObject plateKichenObject)
    {
        if (this is PlateKichenObject)
        {
            plateKichenObject = this as PlateKichenObject;
            return true;
        }
        else
        {
            plateKichenObject = null;
            return false;
        }
    }
}
