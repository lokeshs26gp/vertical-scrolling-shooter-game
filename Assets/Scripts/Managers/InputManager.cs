
using UnityEngine;


public class InputManager : MonoBehaviour,ISystem
{
    public InputConnector inputBroadCaster;

    private bool activate = false;
    public void Initilize()
    {
        activate = true;
    }
   

    void Update()
    {
        if (!activate) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical");

        inputBroadCaster.InvokeAxisEvent(horizontal, vertical);
        
         if (Input.anyKey)
        {
            for(int i =0;i< inputBroadCaster.trackKeyCode.Count;i++)
            {
                if(Input.GetKey(inputBroadCaster.trackKeyCode[i]))
                {
                    inputBroadCaster.InvokeKeyDownEvent(inputBroadCaster.trackKeyCode[i]);
                }
            }
        }
        
    }

    public void DeInitilize()
    {
        activate = false;
        inputBroadCaster.Reset();
    }
    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}


