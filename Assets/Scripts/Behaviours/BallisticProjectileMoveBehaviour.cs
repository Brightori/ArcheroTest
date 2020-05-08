using Components;
using UnityEngine;

namespace Behaviours
{
    class BallisticProjectileMoveBehaviour : MoveBehaviour<IBallisticProjectile>
    {
        private float currentProgress;
        private Vector3 currentPosition;
        private Vector3 updatedPosition;

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
                    state = MoveStates.MOVE;
                    break;
                case MoveStates.MOVE:
                    MoveByTrajectory();
                    break;
            }
        }

        private void MoveByTrajectory()
        {
            currentProgress += movable.MoveSpeed * Time.deltaTime;
            currentPosition = Vector3.Lerp(movable.Transform.position, movable.Target, currentProgress);
            updatedPosition = new Vector3(currentPosition.x, (currentPosition.y + movable.GetTrajectory.Evaluate(currentProgress)), currentPosition.z )
        }
    }
}
