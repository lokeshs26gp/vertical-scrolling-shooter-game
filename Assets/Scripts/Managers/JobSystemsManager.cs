using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JobSystem
{
    public class JobSystemsManager : MonoBehaviour, ISystem
    {
        [Header("Connector")]
        public JobSystemConnector jobSystemConnector;

        private CountDownSystem countDownSystem;
        public void Initilize()
        {
            countDownSystem = new CountDownSystem();
            jobSystemConnector.Register(RegisterCountDown);
            jobSystemConnector.Register(RegisterRepeatCountDown);
            jobSystemConnector.Register(ForceRemoveCountDown);
            countDownSystem.Initilize();
        }

        private void RegisterCountDown(float totalTime,CountDownSystem.OnCompleted callback)
        {
            countDownSystem.AddCountDown(totalTime, callback);
        }
        private void RegisterRepeatCountDown(float totalTime,bool repeat, CountDownSystem.OnCompleted callback)
        {
            countDownSystem.AddCountDown(totalTime, repeat, callback);
        }
        private void ForceRemoveCountDown(CountDownSystem.OnCompleted action)
        {
            countDownSystem.ForceRemoveCountDown(action);
        }
        private void Update()
        {
            countDownSystem.OnUpdate(Time.deltaTime);
        }
        public void DeInitilize()
        {
            countDownSystem.DeInitilize();
            jobSystemConnector.UnRegister(RegisterCountDown);
            jobSystemConnector.UnRegister(RegisterRepeatCountDown);
            jobSystemConnector.UnRegister(ForceRemoveCountDown);
            jobSystemConnector.Reset();
        }
    }
}
