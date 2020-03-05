
namespace JobSystem
{
    public struct CountDown
    {
        public int key;
        public float totalTime;
        public int isCompleted;
        public int repeat;
        public int pause;
        public float cacheTotalTime;
        public CountDown(int pkey,int pRepeat, float ptotaltime,float pcacheTotalTime)
        {
            key = pkey;
            totalTime = ptotaltime;
            cacheTotalTime = pcacheTotalTime;
            isCompleted = cacheTotalTime <= 0.0f ? 1 : 0;
            repeat = pRepeat;
            pause = 0;
        }

        public CountDown Reset()
        {
            cacheTotalTime = totalTime;
            isCompleted = cacheTotalTime <= 0 ? 1 : 0;
            return this;
        }

        public CountDown Pause()
        {
            pause = 1;
            return this;
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