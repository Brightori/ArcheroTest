using Actors;
using Behaviours;
using Commands;
using UnityEngine;
using static CoinSpawnerContoller;

namespace Assets.Scripts.Behaviours
{
    public class SpawnCoinsBehaviour : MonoBehaviour, IBehaviour
    {
        [SerializeField] private int coinNumbers = 3;
        public IActor Actor { get; set; }
        private bool isAlrdyTriggered;

        private void Awake()
        {
            if (TryGetComponent<ICanSetBehaviour>(out var actor))
                actor.AddBehaviour(this);
        }

        public void CommandBehavaiour(ICommand command)
        {
            if (!isAlrdyTriggered && command is IDeadActorCommand deadActorCommand)
            {
                GlobalCommander.Commander.Invoke(new SpawnCoinGlobalCommand { SpawnPosition = transform.position, CoinCount = coinNumbers });
                isAlrdyTriggered = true;
            }
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
    }
}
