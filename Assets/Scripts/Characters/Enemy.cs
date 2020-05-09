using Behaviours;
using Components;
using UnityEngine;

namespace Characters
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
