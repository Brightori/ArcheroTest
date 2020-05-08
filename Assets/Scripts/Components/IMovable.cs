using UnityEngine;

namespace Components
{
    public interface IMovable : IHaveTransform, IHaveMoveSpeed 
    {
    }

    public interface IMoveAndRotate : IMovable, IHaveRotationSpeed
    {
    }

    public enum MoveStates { DEFAULT, WAIT, PATROL, MOVE, PAUSE, UNPAUSE, COMPLETE }

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
