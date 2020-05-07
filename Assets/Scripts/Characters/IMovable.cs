using UnityEngine;

namespace Characters
{
    public interface IMovable : IHaveTransform, IHaveMoveSpeed, IHaveRotationSpeed
    {

    }

    public enum MoveStates { DEFAULT, WAIT, PATROL, FOLLOW, PAUSE, UNPAUSE, }

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
