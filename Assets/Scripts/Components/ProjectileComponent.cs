using Characters;
using UnityEngine;

namespace Components
{
    class ProjectileComponent : MonoBehaviour, IProjectile
    {
        public float Dmg { get; private set; } = 0;
        public float MoveSpeed { get; private set; } = 1;

        public Transform Transform => transform;

        public float RotationSpeed => 0;

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
        }
    }
}
