using Actors;
using UnityEngine;

namespace Behaviours
{
    public abstract class BehaviourContainer<T> : MonoBehaviour where T: IBehaviour
    {
        public abstract T GetValue { get; }

        private void Awake()
        {
            if (TryGetComponent<ICanSetBehaviour>(out var behaviourOwner))
            {
                Init();

                if (GetValue != null)
                    behaviourOwner.AddBehaviour(GetValue);
                else
                    Debug.LogError("нет поведения перемещения в контейнере " + gameObject.name);
            }

            else
                Debug.LogError($"нет владельца для бихейвера {gameObject.name }  {this.name}");
        }

        protected abstract void Init();
    }

    public abstract class MoveBehaviourContainer : BehaviourContainer<IMoveBehaviour> 
    {
    }
}
