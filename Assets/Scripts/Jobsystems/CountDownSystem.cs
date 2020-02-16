
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
                if (jobCopy[i].isCompleted == 0)
                {
                    countdownDictionary[jobCopy[i].key] = jobCopy[i];
                }
                else if (jobCopy[i].isCompleted == 1)
                {
                    CountDown removing;

                    countdownDictionary.TryRemove(jobCopy[i].key, out removing);

                    OnCompleted completed = null;

                    concurrentDictionaryMapper.TryRemove(removing.key, out completed);


                    completed.Invoke(true);

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
            countDownHandle.Complete();

            int uniqueInt = completed.GetHashCode();// countdownDictionary.Count() + 1;
            
            if (concurrentDictionaryMapper.TryAdd(uniqueInt, completed))
            {
                CountDown newCountdown = new CountDown
                {
                    key = uniqueInt,
                    totalTime = ptotalTime
                };
                if (countdownDictionary.TryAdd(uniqueInt, newCountdown))
                {
                   //Debug.Log("Added " + uniqueInt + "TotalTime = " + ptotalTime);
                }
            }
            
        }


        /* public void AddCountDown(string key, float ptotalTime, System.Action<string, float> completed, bool isUpdateFrame)
         {
             countDownHandle.Complete();

             int uniqueInt = countdownDictionary.Count() + 1;

             if (concurrentDictionaryMapper.TryAdd(uniqueInt, new CountDownRef(key, completed, isUpdateFrame)))
             {
                 CountDown newCountdown = new CountDown
                 {
                     key = uniqueInt,
                     totalTime = ptotalTime
                 };
                 if (countdownDictionary.TryAdd(uniqueInt, newCountdown))
                 {
                     Debug.Log("Added " + key + "TotalTime = " + ptotalTime);
                 }
             }
         }
         public void AddCountDown(float ptotalTime, System.Action<bool> completed)
         {
             countDownHandle.Complete();

             int uniqueInt = countdownDictionary.Count() + 1;

             if (concurrentDictionaryMapper.TryAdd(uniqueInt, new CountDownRef(completed)))
             {
                 CountDown newCountdown = new CountDown
                 {
                     key = uniqueInt,
                     totalTime = ptotalTime
                 };
                 if (countdownDictionary.TryAdd(uniqueInt, newCountdown))
                 {
                    // Debug.Log("Added " + key + "TotalTime = " + ptotalTime);
                 }
             }
         }

         public void ForceRemoveCountDown(string key)
         {
             int jobKey = -1;
             jobKey = concurrentDictionaryMapper.FirstOrDefault(x => x.Value.IsEqual(key)).Key;
             if (jobKey > 0)
             {
                 countDownHandle.Complete();
                 CountDown removing = countdownDictionary[jobKey];
                 removing.totalTime = 0.0f;
                 countdownDictionary[jobKey] = removing;
                 Debug.Log("ForceRemoved = " + key);
             }

         }
         */
        public void ForceRemoveCountDown(OnCompleted action)
        {
            
            int jobKey = -1;
            jobKey = concurrentDictionaryMapper.FirstOrDefault(x => x.Value == action).Key;
            if (jobKey > 0)
            {
                countDownHandle.Complete();
                CountDown removing = countdownDictionary[jobKey];
                removing.totalTime = 0.0f;
                countdownDictionary[jobKey] = removing;

            }
            //else
             //   Debug.LogError("ManagedError-No key present in ForceRemoveCountDown");

        }
        #endregion

    }
}