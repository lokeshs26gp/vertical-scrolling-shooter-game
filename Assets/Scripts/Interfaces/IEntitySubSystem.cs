using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntitySubSystem
{
    void Initilize(Entity.Entity entity);
    void DeInitilize();

    void Reset();
}