using System;
using UnityEngine;

namespace Characters
{
    public abstract class Actor : MonoBehaviour 
    {
        private AttackBehaviour<ICanAttack> attackBehaviour;
        private MoveBehaviour<IMovable> moveBehaviour;

        protected bool isHaveAttack { get; private set; }
        protected bool isHaveMove { get; private set; }

        private void Awake()
        {
            Init();
        }

        protected abstract void Init();

        protected void SetAttackBehaviour(AttackBehaviour<ICanAttack> attackBehaviour) 
        {
            if (attackBehaviour == null)
            {
                Debug.LogError("нет валидного аттак бихейвера на смену " + gameObject.name);
                return;
            }


            if (this.attackBehaviour != null)
                this.attackBehaviour.Dispose();

            this.attackBehaviour = attackBehaviour;
            isHaveAttack = true;
        }
        
        protected void SetMoveBehaviour(MoveBehaviour<IMovable> moveBehaviour) 
        {
            if (moveBehaviour == null)
            {
                Debug.LogError("нет валидного мув бихейвера на смену " + gameObject.name);
                return;
            }

            if (this.moveBehaviour != null)
                this.moveBehaviour.Dispose();

            this.moveBehaviour = moveBehaviour;
            isHaveMove = true;
        }

        protected virtual void Update()
        {
            if (isHaveAttack)
                attackBehaviour.Update();

            if (isHaveMove)
                moveBehaviour.Update();
        }
    }

    public class Player : Actor
    {

    }


    public abstract class Behaviour : IDisposable
    {
        public abstract void Update();
        public abstract void Pause();
        public abstract void UnPause();

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public abstract class AttackBehaviour<T> : Behaviour where T : ICanAttack
    {
        protected T attacker;
        protected AttackStates state;

        protected AttackBehaviour(T attacker)
        {
            this.attacker = attacker;
        }
    }

    public abstract class MoveBehaviour<T> : Behaviour where T: IMovable
    {
        protected T movable;
        protected MoveStates state;

        protected MoveBehaviour(T movable)
        {
            this.movable = movable;
        }
    }

    public enum AttackStates { DEFAULT, WAIT, ATTACK, ENDATTACK, PAUSE, UNPAUSE, }
    public enum MoveStates { DEFAULT, WAIT, PATROL, FOLLOW, PAUSE, UNPAUSE, }

    public interface IMovable : IHaveTransform, IHaveMoveSpeed, IHaveRotationSpeed
    {

    }

    public interface IHaveTransform
    {
        Transform Transform { get; }
    }

    public interface IHaveMoveSpeed
    {
        float MoveSpeed { get; }
    }

    public interface IHaveRotationSpeed
    {
        float RotationSpeed { get; }
    }

    public interface ICanAttack
    {

    }
}
