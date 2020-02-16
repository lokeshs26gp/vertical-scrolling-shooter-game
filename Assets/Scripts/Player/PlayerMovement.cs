using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{

    [Header("Player Tested bounderies")]
    public Vector2 viewportXMinMax = new Vector2(0.0f,0.8f);
    public Vector2 viewportYMinMax = new Vector2(0.1f,1.0f);
        
    public override void Set(Vector2 direction,float speed)
    {
        nextPosition = transform.position;
        nextPosition.x += direction.x;
        nextPosition.y += direction.y;

        Vector3 viewport = Camera.main.WorldToViewportPoint(nextPosition);
        viewport.x = Mathf.Clamp(viewport.x, viewportXMinMax.x, viewportXMinMax.y);
        viewport.y = Mathf.Clamp(viewport.y, viewportYMinMax.x, viewportYMinMax.y);

        nextPosition = Camera.main.ViewportToWorldPoint(viewport);
        //this.speed = speed;
        canMove = true;
    }
    public override void MovementUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }
   
}
