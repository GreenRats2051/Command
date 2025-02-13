using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class CommandInvoker : MonoBehaviour
    {
        private const int MaxCommandHistory = 5;
        private Queue<ICommand> commandQueue = new Queue<ICommand>();
        private Queue<ICommand> rightClickCommandQueue = new Queue<ICommand>();

        private ICommand spawnCommand;
        private ICommand moveCommand;

        [SerializeField] GameObject prefabToSpawn;
        [SerializeField] Transform characterTransform;

        private void Start()
        {
            spawnCommand = new SpawnPrefabCommand(prefabToSpawn);
            moveCommand = new MoveCharacterCommand(characterTransform);
        }

        public void ExecuteCommand(Vector2 position)
        {
            moveCommand.Invoke(position);
            AddToCommandHistory(moveCommand);
        }

        public void AddRightClickCommand(Vector2 position)
        {
            ICommand commandClone = spawnCommand;
            rightClickCommandQueue.Enqueue(commandClone);
        }

        public void ExecuteRightClickCommands()
        {
            while (rightClickCommandQueue.Count > 0)
            {
                ICommand command = rightClickCommandQueue.Dequeue();
                command.Invoke(Vector2.zero);
                AddToCommandHistory(command);
            }
        }

        public void AddToCommandHistory(ICommand command)
        {
            if (commandQueue.Count >= MaxCommandHistory)
            {
                commandQueue.Dequeue();
            }
            commandQueue.Enqueue(command);
        }

        public void Undo()
        {
            if (commandQueue.Count > 0)
            {
                ICommand lastCommand = commandQueue.Dequeue();
                lastCommand.Undo();
                Debug.Log("Команда отменена: " + lastCommand.GetType().Name);
            }
            else
            {
                Debug.Log("Нет команд для отмены.");
            }
        }
    }
}