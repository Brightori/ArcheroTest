using UnityEngine;

namespace Commands
{
    public struct RotateToTargetCommand : ICommandRotateToTarget
    {
        public Vector3 Target { get; set; }
    }

    public interface ICommandRotateToTarget : ICommand
    {
        Vector3 Target { get; }
    }
}
