using UnityEngine;

namespace Components
{
    class BallisticProjectileComponent : MonoBehaviour, IBallisticProjectile
    {
        [SerializeField] private AnimationCurve trajectory = default;
        private Vector3 target;


        public float Dmg { get; private set; } = 0;
        public float MoveSpeed { get; private set; } = 1;

        public Transform Transform => transform;

        public float RotationSpeed => 0;


        public AnimationCurve GetTrajectory => trajectory;

        public bool IsReady { get; private set; }

        public Vector3 Target => target;

        public void SetDmg(float dmg)
        {
            Dmg = dmg;
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            MoveSpeed = moveSpeed;
        }

        public void SetTarget(Vector3 target)
        {
            this.target = target;
            IsReady = true;
        }
    }
}
