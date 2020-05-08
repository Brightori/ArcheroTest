using UnityEngine;

namespace Components
{
    public class LevelComponent : MonoBehaviour
    {
        public FloorComponent[] floorComponents { get; private set; }

        private void Awake()
        {
            floorComponents = GetComponentsInChildren<FloorComponent>();
        }
    }
}
