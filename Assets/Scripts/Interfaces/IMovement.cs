using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
    void Set(Vector2 direction,float speed);
    void MovementUpdate();

    void LookAt(Vector2 position);
    void Reset();
   
}
