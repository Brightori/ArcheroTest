using Behaviours;
using Components;

namespace Behaviours
{
    public class ProjectileMoveBehaviour : MoveBehaviour<IMovable>
    {
        public ProjectileMoveBehaviour(IMovable movable) : base(movable)
        {
        }

        public override void Pause()
        {
            state = MoveStates.PAUSE;
        }

        public override void UnPause()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}