using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
public class InputConnector : Connector
{
    public const string fileName = "InputConnector";

    public List<KeyCode> trackKeyCode;

    public delegate void AxisEvent(float horizontal,float vertical);
    public delegate void KeyDownEvent(KeyCode key);
    
    private event AxisEvent OnAxisEvent;
    private event KeyDownEvent OnkeyDownEvent;

    public override void Reset()
    {
        OnAxisEvent = null;
        OnkeyDownEvent = null;
    }
    public void RegisterAxisListener(AxisEvent axisEvent)
    {
        OnAxisEvent += axisEvent;
    }
    public void UnregisterListener(AxisEvent axisEvent)
    {
        OnAxisEvent -= axisEvent;
    }

    public void InvokeAxisEvent(float horizontal, float vertical)
    {
        if(OnAxisEvent!=null)
            OnAxisEvent(horizontal, vertical);
    }


    public void RegisterKeyDownListener(KeyDownEvent keyDownEvent)
    {
        OnkeyDownEvent += keyDownEvent;
    }
    public void UnregisterKeyDownListener(KeyDownEvent keyDownEvent)
    {
        OnkeyDownEvent -= keyDownEvent;
    }

    public void InvokeKeyDownEvent(KeyCode code)
    {
        if (OnkeyDownEvent != null)
            OnkeyDownEvent(code);
    }

}

