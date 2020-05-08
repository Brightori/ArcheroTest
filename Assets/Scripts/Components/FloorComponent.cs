using UnityEngine;

namespace Components
{
    public class FloorComponent : MonoBehaviour
    {
        [SerializeField] private FloorComponentStates componentStates = FloorComponentStates.AVAILABLE;
        public FloorComponentStates ComponentStates => componentStates;
    }
}
