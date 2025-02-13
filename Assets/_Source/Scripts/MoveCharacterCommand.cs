using UnityEngine;

namespace Scripts
{
    public class MoveCharacterCommand : ICommand
    {
        private Transform characterTransform;
        private Vector2 previousPosition;

        public MoveCharacterCommand(Transform characterTransform)
        {
            this.characterTransform = characterTransform;
        }

        public void Invoke(Vector2 position)
        {
            previousPosition = characterTransform.position;
            characterTransform.position = position;
        }

        public void Undo()
        {
            characterTransform.position = previousPosition;
        }
    }
}