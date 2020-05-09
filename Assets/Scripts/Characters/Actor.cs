using Components;
using UnityEngine;
using Behaviours;
using System.Collections.Generic;

namespace Characters
{
    public abstract class Actor: MonoBehaviour, ICanBePaused, ICanSetBehaviour
    {
        private List<IBehaviour> behaviours = new List<IBehaviour>(20);

        private void Start()
        {
            GlobalCommander.Commander.RegisterObjectByEvent<ICanBePaused>(this, true);
            Init();
        }

        protected abstract void Init();

        public void AddBehaviour(IBehaviour behaviour) 
        {
            if (behaviours.Contains(behaviour))
                return;
            behaviours.Add(behaviour);
        }

        protected virtual void Update()
        {
            for (int i = 0; i < behaviours.Count; i++)
                behaviours[i].Update();
        }

        public void SetPause(bool state)
        {
            if (state)
                foreach (var b in behaviours)
                    b.Pause();
            else
                foreach (var b in behaviours)
                    b.UnPause();
        }
    }

    public interface ICanBePaused
    {
        void SetPause(bool state);
    }
}
