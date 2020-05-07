using Characters;
using UnityEngine;

namespace Behaviours
{
    public abstract class BehaviourContainer<T> : MonoBehaviour where T: IBehaviour
    {
        public abstract T GetValue { get; }
    }

    public abstract class MoveBehaviourContainer : BehaviourContainer<IMoveBehaviour> 
    {
        private ICanSetMoveBehaviour behaviourOwner;

        private void Awake()
        {
            if (TryGetComponent(out behaviourOwner))
            {
                Init();

                if (GetValue != null)
                    behaviourOwner.InitMoveBehaviour(GetValue);
                else
                    Debug.LogError("нет поведения перемещения в контейнере " + gameObject.name);
            }
                
            else
                Debug.LogError($"нет владельца для бихейвера {gameObject.name }  {this.name}");
        }

        protected abstract void Init();
    }
}
