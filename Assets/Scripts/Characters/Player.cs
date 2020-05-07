using UnityEngine;
namespace Characters
{
    public class Player : Actor
    {
        protected override void Init()
        {
            if (TryGetComponent<IMovable>(out var movable))
                SetMoveBehaviour(new PlayerStandartMoveBehaviour(movable));
            else
                Debug.LogError("нет компонента перемещения у " + gameObject.name); 
            
            if (TryGetComponent<ICanAttack>(out var attacker))
                SetAttackBehaviour(new PlayerStandartAttackBehaviour(attacker));
            else
                Debug.LogError("нет компонента атаки у " + gameObject.name);
        }
    }
}
