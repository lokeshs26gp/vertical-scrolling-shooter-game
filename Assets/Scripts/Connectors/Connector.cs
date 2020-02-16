using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
public abstract class Connector : ScriptableObject
{
    public const string prefixPath = "Connectors/"; 

    public virtual void Reset() { }

    private void Awake()
    {
       Reset();
    }
 
    private void OnDisable()
    {
        Reset();
    }
}

