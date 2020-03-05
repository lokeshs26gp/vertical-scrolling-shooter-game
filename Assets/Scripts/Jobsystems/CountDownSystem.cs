
using Unity.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Unity.Jobs;
using System.Linq;

namespace JobSystem
{
    public class CountDownSystem
    {
        public delegate void OnCompleted(bool done);

        private ConcurrentDictionary<int, CountDown> countdownDictionary;

        private ConcurrentDictionary<int, OnCompleted> concurrentDictionaryMapper;

        private CountDownjob countDownjob;

        private JobHandle countDownHandle;

        public void Initilize()
        {
            countdownDictionary = new ConcurrentDictionary<int, CountDown>();
            concurrentDictionaryMapper = new ConcurrentDictionary<int, OnCompleted>();
        }

        public void OnUpdate(float deltaTime)
        {
            int count = countdownDictionary.Count();

            if (count <= 0)
                return;

            NativeArray<CountDown> copy = new NativeArray<CountDown>(countdownDictionary.Values.ToArray(), Allocator.TempJob);

             countDownjob = new CountDownjob
            {
                dt = deltaTime,
                countDowns = copy
            };
            countDownHandle = countDownjob.Schedule(count, 128);

            countDownHandle.Complete();

            UpdateCountDowns(countDownjob.countDowns);

            copy.Dispose();


        }

        private void UpdateCountDowns(NativeArray<CountDown> jobCopy)
        {
            for (int i = 0; i < jobCopy.Count(); i++)
            {
                if (!countdownDictionary.ContainsKey(jobCopy[i].key))
                    continue;
                
                if (jobCopy[i].isCompleted == 0)
                {
                    countdownDictionary[jobCopy[i].key] = jobCopy[i];
                }
                else if (jobCopy[i].isCompleted == 1)
                {
                    CountDown removing;

                    if (jobCopy[i].repeat == 0)
                    {
                        countdownDictionary.TryRemove(jobCopy[i].key, out removing);

                        OnCompleted completed = null;

                        concurrentDictionaryMapper.TryRemove(removing.key, out completed);

                        completed.Invoke(true);

                        //Debug.Log("Removed Key " + jobCopy[i].key);

                    }
                    else if (jobCopy[i].repeat == 1)
                    { 
                        concurrentDictionaryMapper[countdownDictionary[jobCopy[i].key].key].Invoke(true);
                        countdownDictionary[jobCopy[i].key] = jobCopy[i].Reset();
                      
                    }

                }

            }

        }
        public void DeInitilize()
        {
            concurrentDictionaryMapper.Clear();
            countdownDictionary.Clear();

        }

        #region PublicFunctions

        public void AddCountDown(float ptotalTime, OnCompleted completed)
        {
            int uniqueInt = completed.GetHashCode();

            if (concurrentDictionaryMapper.TryAdd(uniqueInt, completed))
            {
                CountDown newCountdown = new CountDown
                {
                    key = uniqueInt,
                    totalTime = ptotalTime,
                    cacheTotalTime = ptotalTime,
                    repeat = 0
                };
                if (countdownDictionary.TryAdd(uniqueInt, newCountdown))
                {
                    //Debug.Log("Added " + uniqueInt + "TotalTime = " + ptotalTime);
                }
            }
            else if (concurrentDictionaryMapper.ContainsKey(uniqueInt))
            {
               // Debug.Log("already contains key "+ uniqueInt);
            }
            
        }
        public void AddCountDown(float ptotalTime,bool pRepeat, OnCompleted completed)
        {
            int uniqueInt = completed.GetHashCode();// countdownDictionary.Count() + 1;

            CountDown newCountdown = new CountDown
            {
                key = uniqueInt,
                totalTime = ptotalTime,
                cacheTotalTime = ptotalTime,
                repeat = pRepeat ? 1 : 0
            };   
            if (countdownDictionary.TryAdd(uniqueInt, newCountdown))
            { 
                if(concurrentDictionaryMapper.TryAdd(uniqueInt, completed))
                {
                   // Debug.Log("Added " + uniqueInt + "TotalTime = " + ptotalTime);
                }
            }
            else if (countdownDictionary.ContainsKey(uniqueInt))
            {
                if(countdownDictionary.TryUpdate(uniqueInt, newCountdown, countdownDictionary[uniqueInt]))
                {
                   // Debug.Log("Already existing key " + uniqueInt + " updated!");
                }
            }
            

        }

        public void ForceRemoveCountDown(OnCompleted action)
        {

            int jobKey = action.GetHashCode();
            if (countdownDictionary.ContainsKey(jobKey))
            {

                if (countdownDictionary.TryRemove(jobKey, out CountDown removed))
                {
                    if( concurrentDictionaryMapper.TryRemove(removed.key, out OnCompleted removedCallBack))
                    {
                        //Debug.Log("successfully removed =" + jobKey);
                    }
                }

            }
           // else
               // Debug.LogError("key " + jobKey + "doesnot exists!");
                


        }
#endregion

}
}
 