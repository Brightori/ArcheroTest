using Components;

namespace Behaviours
{
    public abstract class AttackBehaviour<T> : BaseBehaviour, IAttackBehaviour where T : ICanAttack
    {
        protected T attacker;
        protected AttackStates state;

        protected AttackBehaviour(T attacker)
        {
            this.attacker = attacker;
        }
    }

    public interface IAttackBehaviour : IBehaviour
    { 
    
    }
}
