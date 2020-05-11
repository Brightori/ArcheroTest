using Actors;
using Commands;
using UnityEngine;

namespace Behaviours
{
    class CleanBehaviour : MonoBehaviour, IBehaviour
    {
        public IActor Actor { get; set; }

        private void Awake()
        {
            if (TryGetComponent<ICanSetBehaviour>(out var actor))
                actor.AddBehaviour(this);
            else
                Debug.LogError("нет актора на " + gameObject.name);
        }

        public void CommandBehavaiour(ICommand command)
        {
            if (command is ICleanCommand)
                Actor.Dispose();
        }

        public void Dispose()
        {
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
    }
}
