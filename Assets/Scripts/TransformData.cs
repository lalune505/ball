using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransformData : ScriptableObject
{
    public TransformItem item;
}

[System.Serializable]
public class TransformItem
{
    public float[] x;
    public float[] y;
    public float[] z;

}
