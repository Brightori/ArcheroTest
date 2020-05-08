using Behaviours;

namespace Characters
{
    class Ability : Actor, ICanSetMoveBehaviour
    {
        public void InitMoveBehaviour(IMoveBehaviour moveBehaviour) => SetMoveBehaviour(moveBehaviour);

        protected override void Init()
        {
        }
    }
}
