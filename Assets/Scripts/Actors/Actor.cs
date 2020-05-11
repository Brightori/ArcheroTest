using Behaviours;
using Commands;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actors
{
    public abstract class Actor: MonoBehaviour, IActor
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
            behaviour.Actor = this;
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

        public void Command(ICommand command)
        {
            foreach (var b in behaviours)
                b.CommandBehavaiour(command);
        }

        public void Dispose()
        {
            foreach (var b in behaviours)
                b.Dispose();

            Destroy(gameObject);
        }
    }

    public interface ICanBePaused
    {
        void SetPause(bool state);
    }

    public interface IActor : IDisposable, ICanBePaused, ICanSetBehaviour
    {
        void Command(ICommand command);
    }
}
