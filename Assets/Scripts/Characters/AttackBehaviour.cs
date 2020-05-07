namespace Characters
{
    public abstract class AttackBehaviour<T> : BaseBehaviour where T : ICanAttack
    {
        protected T attacker;
        protected AttackStates state;

        protected AttackBehaviour(T attacker)
        {
            this.attacker = attacker;
        }
    }
}
