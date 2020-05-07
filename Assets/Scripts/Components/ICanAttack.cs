using System.Threading.Tasks;
using UnityEngine;

namespace Components
{
    public interface ICanAttack : IDmg, IAttackInterval, IAttackProjectile, IAttackMoveSpeed, IShootPosition, IHaveTransform
    {
        bool IsReadyForAttack { get; }
    }

    public interface IShootPosition
    {
        Vector3 ShootPosition { get; }
    }

    public interface IAttackMoveSpeed
    {
        float AttackMoveSpeed { get; }
    }

    public interface IProjectile : IDmg, IMovable
    {
        void SetDmg(float dmg);
        void SetMoveSpeed(float moveSpeed);
        void SetTarget(Vector3 target);
    }

    public interface IDmg
    {
        float Dmg { get; }
    }

    public interface IAttackInterval
    {
        float AttackInterval { get; }
    }

    public interface IAttackProjectile
    {
        Task<GameObject> GetProjectile(Vector3 position);
    }
}
