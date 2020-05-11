using Actors;
using Commands;

namespace Behaviours
{
    public class AddCoinsToPlayerBehaviour : BehaviourContainer<IBehaviour>, IBehaviour
    {
        public override IBehaviour GetValue => this;
        public IActor Actor { get; set; }

        public void CommandBehavaiour(ICommand command)
        {
        }

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IPlayer>(out var player))
                Actor.Command(new CleanCommand());
        }

        public void Dispose()
        {
        }

        public void Pause()
        {
        }

        public void UnPause()
        {
        }

        void IBehaviour.Update()
        {
        }

        protected override void Init()
        {
        }
    }
}
