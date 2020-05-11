using Behaviours;
using Components;
using UnityEngine;

namespace Actors
{
    class Enemy : Actor
    {
        protected override void Init()
        {
     
        }
    }

    public interface ICanSetBehaviour
    {
        void AddBehaviour(IBehaviour behaviour);
    }
}
