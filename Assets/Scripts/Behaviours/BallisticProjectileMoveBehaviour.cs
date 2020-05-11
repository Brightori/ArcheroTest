using Commands;
using Components;
using UnityEngine;

namespace Behaviours
{
    public class BallisticProjectileMoveBehaviour : MoveBehaviour<IBallisticProjectile>
    {
        private float currentProgress;
        private float endProgress;
        private Vector3 currentPosition;
        private Vector3 startPosition;
        private float lenght;
        private float endTime = 0;
        private float trajectoryCoeff;

        public BallisticProjectileMoveBehaviour(IBallisticProjectile movable) : base(movable)
        {
        }

        public override void Pause()
        {
        }

        public override void UnPause()
        {
        }

        public override void Update()
        {
            switch (state)
            {
                case MoveStates.DEFAULT:
                    if (!movable.IsReady)
                        return;

                    startPosition = movable.Transform.position;
                    lenght = (movable.Target - movable.Transform.position).magnitude;
                    endTime = (lenght / movable.MoveSpeed);
                    state = MoveStates.MOVE;
                    break;
                case MoveStates.MOVE:
                    MoveByTrajectory();
                    break;
                case MoveStates.COMPLETE:
                    break;
            }
        }

        private void MoveByTrajectory()
        {
            currentProgress += movable.MoveSpeed * Time.deltaTime;
            endProgress = currentProgress/endTime;
            currentPosition = Vector3.Lerp(startPosition, movable.Target, endProgress);
            movable.Transform.position = currentPosition;
            
            trajectoryCoeff = endProgress * 2f - 1f;
            movable.Transform.position += Vector3.up * movable.GetTrajectoryHeight * (1f - trajectoryCoeff * trajectoryCoeff);

            if (endProgress >= 1)
            {
                Actor.Command(new CleanCommand());
                state = MoveStates.COMPLETE;
            }
        }
    }
}