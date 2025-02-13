using UnityEngine;

namespace Scripts
{
    public class SpawnPrefabCommand : ICommand
    {
        private GameObject prefab;
        private GameObject spawnedObject;

        public SpawnPrefabCommand(GameObject prefab)
        {
            this.prefab = prefab;
        }

        public void Invoke(Vector2 position)
        {
            spawnedObject = Object.Instantiate(prefab, position, Quaternion.identity);
        }

        public void Undo()
        {
            if (spawnedObject != null)
            {
                Object.Destroy(spawnedObject);
            }
        }
    }
}