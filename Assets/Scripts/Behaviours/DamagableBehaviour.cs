using Actors;
using Commands;
using Components;
using UnityEngine;
using UnityEngine.Assertions;
using static CoinSpawnerContoller;

namespace Behaviours
{
    class DamagableBehaviour : MonoBehaviour, IDamagableBehaviour
    {
        private IHealthComponent healthComponent;

        public IActor Actor { get; set; }

        private void Awake()
        {
            healthComponent = GetComponent<IHealthComponent>();
            Assert.IsNotNull(healthComponent, "нет компонента Health у " + gameObject.name);

            if (TryGetComponent<ICanSetBehaviour>(out var behaviourOwner))
                behaviourOwner.AddBehaviour(this);
            else
                Debug.LogError("нет актора у " + gameObject.name);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDmg>(out var dmg))
            {
                healthComponent.RemoveHealth(dmg.Dmg);
                GlobalCommander.Commander.Invoke(new ShakeCameraGlobalCommand());

                if (healthComponent.IsDead)
                {
                    Actor.Command(new ActorDeadCommand());
                    Dispose();
                }
            }
        }

        public void Dispose()
        {
            GlobalCommander.Commander.RegisterObjectByEvent(gameObject, false);
            Destroy(gameObject);
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

        public void CommandBehavaiour(ICommand command)
        {
        }
    }

    public interface IDamagableBehaviour : IBehaviour { }

}
