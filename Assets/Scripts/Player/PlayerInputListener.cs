using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

public class PlayerInputListener : MonoBehaviour,IEntitySubSystem
{
    public InputConnector inputEvent;

    public IMovement playerMovement;
    public IFire playerFire;

    
    private void OnEnable()
    {
       
    }
    private void AxisMovement(float horizontal, float vertical)
    {
        playerMovement.Set(new Vector2(horizontal, vertical),0.0f);
    }
    private void OnKeyDownEvent(KeyCode code)
    {
        switch (code)
        {
            case KeyCode.F:
            case KeyCode.Space:
               // Debug.Log("Fire");
                playerFire.Shoot();
                break;
        }
    }
    private void OnDisable()
    {
        inputEvent.UnregisterListener(AxisMovement);
        inputEvent.UnregisterKeyDownListener(OnKeyDownEvent);
    }

    public void Initilize(Entity.Entity entity)
    {
        playerMovement = entity.GetMovement;
        playerFire = ((PlayerEntity)entity).GetFire;
        inputEvent.RegisterAxisListener(AxisMovement);
        inputEvent.RegisterKeyDownListener(OnKeyDownEvent);
    }

    public void DeInitilize()
    {
       
    }

    public void Reset()
    {
       
    }
}
