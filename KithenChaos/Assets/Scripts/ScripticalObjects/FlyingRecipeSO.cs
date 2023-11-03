using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FlyingRecipeSO : ScriptableObject
{
    public KicthenObjectSO input;
    public KicthenObjectSO output;
    public float timerFlyingMax;
}
