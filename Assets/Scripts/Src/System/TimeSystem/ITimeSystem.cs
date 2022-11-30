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
    }

    public class DelayTask
    {
        public float Seconds { get; set; }
        public Action OnDelayFinish { get; set; }
        public float StartSeconds { get; set; }
        public float FinishSeconds { get; set; }
        public DelayTaskState State { get; set; }
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
        private LinkedList<DelayTask> mDelayTasks = new();

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

        protected override void OnInit()
        {
            // 创建一个GO用于挂载TimeSystemUpdateBehaviour
            var updateBehaviourGO = new GameObject(nameof(TimeSystemUpdateBehaviour));
            var updateBehaviour = updateBehaviourGO.AddComponent<TimeSystemUpdateBehaviour>();

            updateBehaviour.OnUpdate += OnUpdate;

            CurrentSeconds = 0;
        }

        private void OnUpdate()
        {
            // 计时
            CurrentSeconds += Time.deltaTime;

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
    }
}
