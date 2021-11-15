using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver //load or unload memory 
{
    public float initialValue;

    [HideInInspector]
    public float RuntimeValue;
    public void OnAfterDeserialize() // unload everything
    {
        RuntimeValue = initialValue;
    }
    public void OnBeforeSerialize()
    {

    }
}
