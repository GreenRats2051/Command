using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class CommandInvoker : MonoBehaviour
    {
        private const int MaxCommandHistory = 5;
        private List<ICommand> commandHistory = new List<ICommand>();
        private List<ICommand> rightClickCommandHistory = new List<ICommand>();
        private List<Vector2> mousePosition = new List<Vector2>();

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
            mousePosition.Add(position);
            rightClickCommandHistory.Add(commandClone);
        }

        public void ExecuteRightClickCommands()
        {
            while (rightClickCommandHistory.Count > 0)
            {
                ICommand command = rightClickCommandHistory[0];
                command.Invoke(mousePosition[0]);
                AddToCommandHistory(command);
                rightClickCommandHistory.RemoveAt(0);
            }
        }

        public void AddToCommandHistory(ICommand command)
        {
            if (commandHistory.Count >= MaxCommandHistory)
            {
                commandHistory.RemoveAt(0);
            }
            commandHistory.Add(command);
        }

        public void Undo()
        {
            if (commandHistory.Count > 0)
            {
                ICommand lastCommand = commandHistory[commandHistory.Count - 1];
                lastCommand.Undo();
                commandHistory.RemoveAt(commandHistory.Count - 1);
                Debug.Log("Команда отменена: " + lastCommand.GetType().Name);
            }
            else
            {
                Debug.Log("Нет команд для отмены.");
            }
        }
    }
}
