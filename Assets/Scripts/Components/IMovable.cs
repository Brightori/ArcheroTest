using UnityEngine;

namespace Components
{
    public interface IMovable : IHaveTransform, IHaveMoveSpeed
    {
    }

    public interface IMoveAndRotate : IMovable, IHaveRotationSpeed, IHaveStoppingDistance
    {
    }

    public interface IBounceMove : IMovable, IHaveRotationSpeed
    {
        Rigidbody Rigidbody { get; }
        float BounceRadius { get; }
        float Force { get; }
    }

    public interface IHaveStoppingDistance
    {
        float StoppingDistance { get; }
    }

    public enum MoveStates { DEFAULT, WAIT, MOVE, PAUSE, UNPAUSE, COMPLETE }

    public interface IHaveTransform
    {
        Transform Transform { get; }
    }

    public interface IHaveMoveSpeed
    {
        float MoveSpeed { get; }
    }

    public interface IHaveRotationSpeed
    {
        float RotationSpeed { get; }
    }
}
