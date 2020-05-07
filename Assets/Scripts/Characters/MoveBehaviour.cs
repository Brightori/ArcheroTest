namespace Characters
{
    public abstract class MoveBehaviour<T> : BaseBehaviour where T : IMovable
    {
        protected T movable;
        protected MoveStates state;

        protected MoveBehaviour(T movable)
        {
            this.movable = movable;
        }
    }
}
