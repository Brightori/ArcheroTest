namespace Characters
{
    public class PlayerStandartAttackBehaviour : AttackBehaviour<ICanAttack>
    {
        public PlayerStandartAttackBehaviour(ICanAttack attacker) : base(attacker)
        {
        }

        public override void Pause()
        {
            state = AttackStates.PAUSE;
        }

        public override void UnPause()
        {
            state = AttackStates.UNPAUSE;
        }

        public override void Update()
        {
            switch (state)
            {
                case AttackStates.DEFAULT:
                    break;
                case AttackStates.WAIT:
                    break;
                case AttackStates.ATTACK:
                    break;
                case AttackStates.ENDATTACK:
                    break;
                case AttackStates.PAUSE:
                    break;
                case AttackStates.UNPAUSE:
                    break;
            }
        }
    }
}
