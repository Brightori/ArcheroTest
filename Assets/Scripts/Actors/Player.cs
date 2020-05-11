using Behaviours;
using Components;
using UnityEngine;
namespace Actors
{
    public class Player : Actor, IPlayer
    {
        protected override void Init()
        {
            GlobalCommander.Commander.RegisterInject<Player>(this);

            if (TryGetComponent<IMoveAndRotate>(out var movable))
                AddBehaviour(new PlayerStandartMoveBehaviour(movable));
            else
                Debug.LogError("нет компонента перемещения у " + gameObject.name); 
            
            if (TryGetComponent<ICanAttack>(out var attacker))
                AddBehaviour(new PlayerStandartAttackBehaviour(attacker));
            else
                Debug.LogError("нет компонента атаки у " + gameObject.name);
        }
    }

    public interface IPlayer : IActor { }
}
