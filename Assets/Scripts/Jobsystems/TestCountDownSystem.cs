using UnityEngine;
using System.Collections;
using JobSystem;
public class TestCountDownSystem : MonoBehaviour
{
   /* CountDownSystem system;
    // Start is called before the first frame update
    IEnumerator Start()
    {

        system = new CountDownSystem();
        system.Initilize();
        for (int i = 0; i < 20;i++)
        {
            system.AddCountDown("key" + i,20, HandleAction,true);
            yield return new WaitForSeconds(10);
            system.ForceRemoveCountDown("key"+i);
        }
       

        
    }

    private void Update()
    {
        system.OnUpdate(Time.deltaTime);
    }
    void HandleAction(string obj,float timer)
    {
        Debug.Log("countdown -->"+obj+" = "+timer);
    }
    */
}
