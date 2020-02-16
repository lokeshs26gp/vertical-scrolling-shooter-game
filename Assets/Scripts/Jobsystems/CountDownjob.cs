using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

namespace JobSystem
{
    public struct CountDownjob : IJobParallelFor
    {
        public NativeArray<CountDown> countDowns;
        public float dt;
        public void Execute(int index)
        {
            CountDown count = countDowns[index];
            count.totalTime -= dt;
            countDowns[index] = new CountDown(count.key, count.totalTime);

        }


    }
}