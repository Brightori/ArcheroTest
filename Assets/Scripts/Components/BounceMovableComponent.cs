using UnityEngine;

namespace Components
{
    [DefaultExecutionOrder(-1), RequireComponent(typeof (Rigidbody))]
    class BounceMovableComponent : MonoBehaviour, IBounceMove
    {
        [SerializeField] private float bounceRadius = 3;
        [SerializeField] private float moveSpeed = 3;
        [SerializeField] private float rotationSpeed = 3;
        [SerializeField] private float force = 3;

        public Rigidbody Rigidbody { get; private set;}
        public float BounceRadius => bounceRadius;
        public Transform Transform => transform;
        public float MoveSpeed => moveSpeed;
        public float RotationSpeed => rotationSpeed;

        public float Force => force;
        private void Awake() => Rigidbody = GetComponent<Rigidbody>();
    }
}