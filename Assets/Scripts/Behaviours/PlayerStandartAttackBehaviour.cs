using Commands;
using Components;
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
                    
                    if (!enemyController.TryGetClosestEnemy(attacker.Transform.position, out var target))
                    {
                        CompleteAttack();
                        return;
                    }

                    Actor.Command(new RotateToTargetCommand { Target = target });

                    var go = await attacker.GetProjectile(attacker.ShootPosition); //TODO переписать на пулл

                    if (go.TryGetComponent<IBallisticProjectile>(out var prj))
                        SetupProjectile(prj, target);
                    else
                        Debug.LogError("нет проджектайл компонента");

                    CompleteAttack();
                    break;
                case AttackStates.ENDATTACK:
                    break;
                case AttackStates.PAUSE:
                    break;
                case AttackStates.UNPAUSE:
                    break;
            }
        }

        private void CompleteAttack()
        {
            nextAttackTime = Time.time + attacker.AttackInterval;
            state = AttackStates.WAIT;
        }

        //тут сетапим проджектайл, для демеджа и скорости проджектайла могут быть модификаторы, в будущем будем учитывать это здесь
        private void SetupProjectile(IBallisticProjectile projectile, Vector3 target)
        {
            projectile.DmgOwner = DmgOwner.PLAYERSIDE;
            projectile.SetDmg(attacker.Dmg);
            projectile.SetMoveSpeed(attacker.AttackMoveSpeed);
            projectile.SetTarget(target);
        }
    }
}
