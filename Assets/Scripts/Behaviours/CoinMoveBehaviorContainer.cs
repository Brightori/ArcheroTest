using Components;

namespace Behaviours
{
    public class CoinMoveBehaviorContainer : BehaviourContainer<CoinMoveBehaviour>
    {
        private CoinMoveBehaviour moveBehaviour;

        public override CoinMoveBehaviour GetValue => moveBehaviour;

        protected override void Init()
        {
            if (TryGetComponent<IBounceMove>(out var bounceMove))
                moveBehaviour = new CoinMoveBehaviour(bounceMove);
        }
    }
}