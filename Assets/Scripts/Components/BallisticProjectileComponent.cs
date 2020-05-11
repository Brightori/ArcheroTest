using UnityEngine;

namespace Components
{
    class BallisticProjectileComponent : MonoBehaviour, IBallisticProjectile, IDmg
    {
        [SerializeField] private float trajectoryHeight = 5;
        private Vector3 target;
        public float Dmg { get; private set; } = 0;
        public float MoveSpeed { get; private set; } = 1;
        public Transform Transform => transform;
        public float RotationSpeed => 0;
        public float GetTrajectoryHeight => trajectoryHeight;
        public bool IsReady { get; private set; }
        public Vector3 Target => target;

        public DmgOwner DmgOwner { get; set; } = DmgOwner.DEFAULT;

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
