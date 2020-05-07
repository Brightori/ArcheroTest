using Characters;
using UnityEngine;

namespace Components
{
    public class CharacterMovableComponent : MonoBehaviour, IMoveAndRotate
    {
        [SerializeField] private float moveSpeed = 3;
        [SerializeField] private float rotationSpeed = 3;

        public Transform Transform => transform;
        public float MoveSpeed => moveSpeed;
        public float RotationSpeed => rotationSpeed;
    }
}