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

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                AddRightClickCommand(spawnCommand, mousePosition);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ExecuteCommand(moveCommand, mousePosition);
            }

            if (Input.GetMouseButtonDown(2))
            {
                Undo();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                ExecuteRightClickCommands();
            }
        }

        private void ExecuteCommand(ICommand command, Vector2 position)
        {
            command.Invoke(position);
            AddToCommandHistory(command);
        }

        private void AddRightClickCommand(ICommand command, Vector2 position)
        {
            ICommand commandClone = command;
            rightClickCommandQueue.Enqueue(commandClone);
        }

        private void ExecuteRightClickCommands()
        {
            while (rightClickCommandQueue.Count > 0)
            {
                ICommand command = rightClickCommandQueue.Dequeue();
                command.Invoke(Vector2.zero);
                AddToCommandHistory(command);
            }
        }

        private void AddToCommandHistory(ICommand command)
        {
            if (commandQueue.Count >= MaxCommandHistory)
            {
                commandQueue.Dequeue();
            }
            commandQueue.Enqueue(command);
        }

        private void Undo()
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