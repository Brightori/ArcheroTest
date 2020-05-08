using Components;
using System;
using UnityEngine;

namespace Behaviours
{
    public class PlayerStandartAttackBehaviour : AttackBehaviour<ICanAttack>
    {
        public float nextAttackTime;
        private EnemyController enemyController;

        public PlayerStandartAttackBehaviour(ICanAttack attacker) : base(attacker)
        {
            GlobalCommander.Commander.Inject((EnemyController ctrl) => enemyController = ctrl);
        }

        public override void Pause()
        {
            state = AttackStates.PAUSE;
        }

        public override void UnPause()
        {
            state = AttackStates.UNPAUSE;
        }

        public async override void Update()
        {
            switch (state)
            {
                case AttackStates.DEFAULT:
                    state = AttackStates.WAIT;
                    break;
                case AttackStates.WAIT:
                    if (nextAttackTime < Time.time && attacker.IsReadyForAttack)
                        state = AttackStates.ATTACK;
                    break;
                case AttackStates.ATTACK:
                    var go = await attacker.GetProjectile(attacker.ShootPosition);

                    if (go.TryGetComponent<IBallisticProjectile>(out var prj))
                        SetupProjectile(prj);
                    else
                        Debug.LogError("нет проджектайл компонента");

                    nextAttackTime = Time.time + attacker.AttackInterval;
                    state = AttackStates.WAIT;
                    break;
                case AttackStates.ENDATTACK:
                    break;
                case AttackStates.PAUSE:
                    break;
                case AttackStates.UNPAUSE:
                    break;
            }
        }

        //тут сетапим проджектайл, для демеджа и скорости проджектайла могут быть модификаторы, в будущем будем учитывать это здесь
        private void SetupProjectile(IBallisticProjectile projectile)
        {
            projectile.SetDmg(attacker.Dmg);
            projectile.SetMoveSpeed(attacker.AttackMoveSpeed);
            projectile.SetTarget(enemyController.GetClosestEnemy(attacker.Transform.position));
        }
    }
}
