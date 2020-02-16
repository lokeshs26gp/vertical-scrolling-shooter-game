using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EntityTriggerEnter2DComponent : MonoBehaviour
{
    private ITriggerEnterReceiver triggerReceiver;

    private void Start()
    {
        triggerReceiver = GetComponentInParent<ITriggerEnterReceiver>();
        if (triggerReceiver is default(ITriggerEnterReceiver))
            triggerReceiver = GetComponentInChildren<ITriggerEnterReceiver>();
        if (triggerReceiver is default(ITriggerEnterReceiver))
            triggerReceiver = GetComponent<ITriggerEnterReceiver>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Entity.Entity entity = collision.gameObject.GetComponent<Entity.Entity>();
        if (entity == null)
            entity = collision.gameObject.GetComponentInParent<Entity.Entity>();
        if (entity != null)
        {
            triggerReceiver.OnITriggerEnter(entity,gameObject.name);
        }
    }
}