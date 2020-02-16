
namespace JobSystem
{
    public struct CountDown
    {
        public int key;
        public float totalTime;
        public int isCompleted;

        public CountDown(int pkey, float ptotaltime)
        {
            key = pkey;
            totalTime = ptotaltime;
            isCompleted = totalTime <= 0 ? 1 : 0;
        }


    }

    public class CountDownRef
    {
        private string key;
        private bool updateFrame = false;
        private System.Action<string, float> OnCallback;
        private System.Action<bool> OnCompleted;
        public CountDownRef(string pkey, System.Action<string, float> pcallback, bool onUpdate = false)
        {

            key = pkey;
            OnCallback = pcallback;
            updateFrame = onUpdate;
        }
        public CountDownRef(System.Action<bool> pcallback)
        {

            key = this.GetHashCode().ToString();
            OnCompleted = pcallback;
            updateFrame = false;
        }
        public void OnUpdate(float timer)
        {
            if (updateFrame)
                OnCallback(key, timer);
        }
        public void OnCompletedEvent()
        {
            if (updateFrame) OnCallback(key, 0);

            OnCompleted(true);
        }

        public bool IsEqual(string pkey)
        {
            return key == pkey;
        }

    }
}