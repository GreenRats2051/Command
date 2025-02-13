using UnityEngine;

namespace Scripts
{
    public interface ICommand
    {
        void Invoke(Vector2 position);
        void Undo();
    }
}