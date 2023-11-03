using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipewSO : ScriptableObject
{
    public KicthenObjectSO input;
    public KicthenObjectSO output;
    public int cuttingProgresMax;
}
