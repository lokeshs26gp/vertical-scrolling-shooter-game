using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JobSystem;
[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
public class JobSystemConnector : Connector
{
    public const string fileName = "JobSystemConnector";

    public delegate void RegisterCountDown(float totalTime,CountDownSystem.OnCompleted action);

    public RegisterCountDown registerCountDown;

    public delegate void ForceRemoveCountDown(CountDownSystem.OnCompleted action);

    public ForceRemoveCountDown forceRemoveCountDown;

    public override void Reset()
    {
        registerCountDown = null;
        forceRemoveCountDown = null;
    }

    public void Register(RegisterCountDown action)
    {
        registerCountDown += action;
    }
    public void UnRegister(RegisterCountDown action)
    {
        registerCountDown -= action;
    }

    public void Register(ForceRemoveCountDown action)
    {
        forceRemoveCountDown += action;
    }
    public void UnRegister(ForceRemoveCountDown action)
    {
        forceRemoveCountDown -= action;
    }
}
