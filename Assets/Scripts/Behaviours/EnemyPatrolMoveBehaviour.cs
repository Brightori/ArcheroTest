using Components;
using Configs;
using UnityEngine.Assertions;

namespace Behaviours
{
    public class EnemyPatrolMoveBehaviour : MoveBehaviour<IMoveAndRotate>
    {
        private readonly EnemyPatrolConfig enemyPatrolConfig;

        public EnemyPatrolMoveBehaviour(IMoveAndRotate movable, EnemyPatrolConfig enemyPatrolConfig) : base(movable)
        {
            Assert.IsNotNull(enemyPatrolConfig, "нет конфига для бихейвера патрулирования");
            this.enemyPatrolConfig = enemyPatrolConfig;
        }

        public override void Pause()
        {
        }

        public override void UnPause()
        {
        }

        public override void Update()
        {
            switch (state)
            {
                case MoveStates.DEFAULT:
                    break;
                case MoveStates.WAIT:
                    break;
                case MoveStates.PATROL:
                    break;
                case MoveStates.FOLLOW:
                    break;
                case MoveStates.PAUSE:
                    break;
                case MoveStates.UNPAUSE:
                    break;
            }
        }
    }
}
