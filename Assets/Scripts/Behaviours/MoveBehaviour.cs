using Components;

namespace Behaviours
{
    public abstract class MoveBehaviour<T> : BaseBehaviour, IMoveBehaviour where T : IMovable
    {
        protected T movable;
        protected MoveStates state;

        protected MoveBehaviour(T movable)
        {
            this.movable = movable;
        }
    }

    public interface IMoveBehaviour : IBehaviour { }
}
