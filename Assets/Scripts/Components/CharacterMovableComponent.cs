using Actors;
using UnityEngine;

namespace Components
{
    public class CharacterMovableComponent : MonoBehaviour, IMoveAndRotate
    {
        [SerializeField] private float moveSpeed = 3;
        [SerializeField] private float rotationSpeed = 3;
        [SerializeField] private float stoppingDistance = 0.1f;

        public Transform Transform => transform;
        public float MoveSpeed => moveSpeed;
        public float RotationSpeed => rotationSpeed;
        public float StoppingDistance => stoppingDistance;
    }
}