using Behaviours;
using Components;
using UnityEngine;
namespace Characters
{
    public class Player : Actor
    {
        protected override void Init()
        {
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
}
