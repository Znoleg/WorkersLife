using System;
using System.Collections;
using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Services.GameFactory;
using Main.Scripts.Infrastructure.Services.Input;
using Main.Scripts.Infrastructure.Services.ServiceLocator;
using Main.Scripts.NpcAi;
using UnityEngine;

namespace Main.Scripts.Player
{
    public class PlayerTasker : MonoBehaviour
    {
        private IInputService _inputService;
        private IGameFactory _gameFactory;

        public Status TaskStatus { get; private set; }
        private bool CanCompleteTask => Vector2.Distance(TaskStatus.TaskData.TargetPosition, transform.position) <= 1f;

        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        public void SetTask(TaskData taskData)
        {
            TaskStatus = new Status()
            {
                TaskData = taskData,
                InProcess = true
            };

            StartCoroutine(WaitForTaskComplete());
        }

        private IEnumerator WaitForTaskComplete()
        {
            Task<GameObject> targetHintTask = _gameFactory.GetTargetHint(TaskStatus.TaskData.TargetObject.transform.position);
            yield return new WaitUntil(() => targetHintTask.IsCompleted);
            GameObject targetHint = targetHintTask.Result;

            yield return new WaitUntil(() => TaskStatus.TaskData.TargetObject.Attached);
            targetHint.transform.position = TaskStatus.TaskData.TargetPosition;
            print("Waiting for complete task");

            yield return null;

            bool completed = false;
            while (!completed)
            {
                yield return new WaitUntil(() => _inputService.InteractClicked());
                if (CanCompleteTask)
                {
                    CompleteTask();
                    completed = true;
                }
            }
        }

        private void CompleteTask()
        {
            TaskStatus.TaskData.TargetObject.Detach();
            TaskStatus.TaskData.TargetObject.transform.position = TaskStatus.TaskData.TargetPosition;
            TaskStatus.InProcess = false;
            _gameFactory.ReturnTargetHint();
        }
        
        public class Status
        {
            public TaskData TaskData { get; set; }
            public bool InProcess { get; set; }
        }
    }
}