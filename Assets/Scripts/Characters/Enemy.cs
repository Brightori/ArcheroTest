using Behaviours;
using Components;
using UnityEngine;

namespace Characters
{
    class Enemy : Actor, ICanSetMoveBehaviour, ICanSetAttackBehaviour
    {
        public void InitAttackBehaviour(IAttackBehaviour attackBehaviour) => SetAttackBehaviour(attackBehaviour);
        public void InitMoveBehaviour(IMoveBehaviour moveBehaviour) => SetMoveBehaviour(moveBehaviour);

        protected override void Init()
        {
     
        }
    }

    //суть интерфейсов в том  - чтобы на префабе определять типы поведения, искать класс - behaviourContainer
    public interface ICanSetAttackBehaviour
    {
        void InitAttackBehaviour(IAttackBehaviour attackBehaviour);
    }

    public interface ICanSetMoveBehaviour
    {
        void InitMoveBehaviour(IMoveBehaviour moveBehaviour);
    }
}
