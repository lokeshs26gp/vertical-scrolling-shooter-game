using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    [Range(1.0f, 100.0f)]
    public float speed;
    protected Vector3 nextPosition;
    public Vector2 lookRotation;

    protected  bool canMove = false;

    private Vector3 newDirection;
    public virtual void Set(Vector2 direction,float speed)
    {
        nextPosition = direction;
        
        this.speed = speed;
        Vector2 dir = nextPosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        newDirection = (rotation * Vector3.up).normalized;
        transform.rotation = rotation;
        canMove = true;

    }
    public virtual void MovementUpdate()
    {
        transform.position += newDirection * speed * Time.deltaTime;// Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        
    }

    public virtual void Update()
    {
        if (!canMove) return;
        
        MovementUpdate();

    }

    

    public void Reset()
    {
        canMove = false;
        nextPosition = Vector3.zero;
    }

    public void LookAt(Vector2 position)
    {
        lookRotation = position;
    }
}
