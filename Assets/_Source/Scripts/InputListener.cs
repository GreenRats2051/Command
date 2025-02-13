using Scripts;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    [SerializeField] CommandInvoker commandInvoker;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            commandInvoker.AddRightClickCommand(mousePosition);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            commandInvoker.ExecuteCommand(mousePosition);
        }

        if (Input.GetMouseButtonDown(2))
        {
            commandInvoker.Undo();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            commandInvoker.ExecuteRightClickCommands();
        }
    }
}