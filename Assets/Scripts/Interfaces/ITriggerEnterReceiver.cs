using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerEnterReceiver 
{
    //string GetComponentName { get; }//Onlyfor Debug
    void OnITriggerEnter(Entity.Entity collidedEntity,string fromName);
}
