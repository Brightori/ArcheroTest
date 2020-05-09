using System.Threading.Tasks;
using UnityEngine;

namespace Components
{
    public interface ICanAttack : IComponent, IDmg, IAttackInterval, IAttackProjectile, IAttackMoveSpeed, IShootPosition, IHaveTransform
    {
        bool IsReadyForAttack { get; }
    }

    public interface IComponent { }

    public interface IShootPosition
    {
        Vector3 ShootPosition { get; }
    }

    public interface IAttackMoveSpeed
    {
        float AttackMoveSpeed { get; }
    }

    public interface IBallisticProjectile : IComponent, IDmg, IMovable, IHaveTrajectory, IReady, IHaveTarget
    {
        void SetDmg(float dmg);
        void SetMoveSpeed(float moveSpeed);
        void SetTarget(Vector3 target);
    }

    public interface IHaveTarget  
    {
        Vector3 Target { get; }
    }

    public interface IReady
    {
        bool IsReady { get; }
    }

    public interface IHaveTrajectory
    {
        float GetTrajectoryHeight { get; }
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
