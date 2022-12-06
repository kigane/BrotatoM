using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrotatoM
{
    public interface ITimeSystem : ISystem
    {
        float CurrentSeconds { get; }
        void AddDelayTask(float delayTime, Action onDelayFinish);
        void AddCountDownTask(float totalTime, float interval = 1.0f);
        void Stop();
        void Resume();
    }

    public class DelayTask
    {
        public float Seconds { get; set; }
        public Action OnDelayFinish { get; set; }
        public float StartSeconds { get; set; }
        public float FinishSeconds { get; set; }
        public DelayTaskState State { get; set; }
    }

    public class CountDownTask
    {
        // 总时间
        public float TotalTime { get; set; }
        // 事件发送间隔时间
        public float SendEventInterval { get; set; }
        // 间隔计时器
        public float IntervalTime { get; set; }
    }

    public enum DelayTaskState
    {
        NOT_STARTED,
        STARTED,
        FINISH
    }

    public class TimeSystem : AbstractSystem, ITimeSystem
    {
        /// <summary>
        /// 将Unity的OnUpdate方法委托出去
        /// </summary>
        public class TimeSystemUpdateBehaviour : MonoBehaviour
        {
            public Action OnUpdate;

            private void Update()
            {
                OnUpdate?.Invoke();
            }
        }

        public float CurrentSeconds { get; private set; }
        private readonly LinkedList<DelayTask> mDelayTasks = new();
        private readonly LinkedList<CountDownTask> mCountDownTasks = new();
        private bool mStopped = false;

        public void AddDelayTask(float delayTime, Action onDelayFinish)
        {
            var delayTask = new DelayTask()
            {
                Seconds = delayTime,
                OnDelayFinish = onDelayFinish,
                State = DelayTaskState.NOT_STARTED
            };

            mDelayTasks.AddLast(delayTask);
        }

        public void AddCountDownTask(float totalTime, float interval = 1.0f)
        {
            var countDownTask = new CountDownTask()
            {
                TotalTime = totalTime,
                SendEventInterval = interval,
                IntervalTime = interval
            };

            mCountDownTasks.AddLast(countDownTask);
        }

        public void Stop()
        {
            mStopped = true;
        }

        public void Resume()
        {
            mStopped = false;
        }

        protected override void OnInit()
        {
            // 创建一个GO用于挂载TimeSystemUpdateBehaviour
            var updateBehaviourGO = new GameObject(nameof(TimeSystemUpdateBehaviour));
            var updateBehaviour = updateBehaviourGO.AddComponent<TimeSystemUpdateBehaviour>();
            updateBehaviourGO.AddComponent<DontDestroyOnLoadScript>();

            updateBehaviour.OnUpdate += OnUpdate;

            CurrentSeconds = 0;
        }

        private void OnUpdate()
        {
            // 暂停
            if (mStopped)
                return;

            // 计时
            CurrentSeconds += Time.deltaTime;

            var currCountDownNode = mCountDownTasks.First;
            while (currCountDownNode != null)
            {
                var countDownTask = currCountDownNode.Value;
                // 计时
                countDownTask.TotalTime -= Time.deltaTime;
                countDownTask.IntervalTime -= Time.deltaTime;

                if (countDownTask.IntervalTime <= 0)
                {
                    countDownTask.IntervalTime += countDownTask.SendEventInterval;
                    this.SendEvent(new CountDownIntervalEvent()
                    {
                        Seconds = GetNearestSeconds(countDownTask.TotalTime)
                    });
                }

                if (countDownTask.TotalTime <= 0)
                {
                    this.SendEvent<CountDownOverEvent>();
                    mCountDownTasks.Remove(currCountDownNode);
                }

                currCountDownNode = currCountDownNode.Next;
            }

            var currNode = mDelayTasks.First;
            while (currNode != null)
            {
                var delayTask = currNode.Value;

                if (delayTask.State == DelayTaskState.NOT_STARTED)
                {
                    delayTask.State = DelayTaskState.STARTED;
                    delayTask.StartSeconds = CurrentSeconds;
                    delayTask.FinishSeconds = CurrentSeconds + delayTask.Seconds;
                }
                else if (delayTask.State == DelayTaskState.STARTED)
                {
                    if (CurrentSeconds >= delayTask.FinishSeconds)
                    {
                        delayTask.State = DelayTaskState.FINISH;
                        delayTask.OnDelayFinish();
                        delayTask.OnDelayFinish = null;

                        mDelayTasks.Remove(currNode); // 相比直接用值删除，性能更好
                    }
                }

                currNode = currNode.Next;
            }
        }

        // 获取最近的秒数
        private int GetNearestSeconds(float time)
        {
            return Mathf.Abs(time - (int)time) > 0.9 ? (int)time + 1 : (int)time;
        }
    }
}
