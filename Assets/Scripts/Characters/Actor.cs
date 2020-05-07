using Components;
using UnityEngine;
using Behaviours;

namespace Characters
{
    public abstract class Actor: MonoBehaviour, ICanBePaused 
    {
        private IAttackBehaviour attackBehaviour;
        private IMoveBehaviour moveBehaviour;

        protected bool isHaveAttack { get; private set; }
        protected bool isHaveMove { get; private set; }

        private void Start()
        {
            GlobalCommander.Commander.RegisterObjectByEvent<ICanBePaused>(this, true);
            Init();
        }

        protected abstract void Init();

        protected void SetAttackBehaviour(IAttackBehaviour attackBehaviour) 
        {
            if (attackBehaviour == null)
            {
                Debug.LogError("нет валидного аттак бихейвера " + gameObject.name);
                return;
            }

            if (this.attackBehaviour != null)
                this.attackBehaviour.Dispose();

            this.attackBehaviour = attackBehaviour;
            isHaveAttack = true;
        }

        protected void SetMoveBehaviour(IMoveBehaviour moveBehaviour)
        {
            if (moveBehaviour == null)
            {
                Debug.LogError("нет валидного мув бихейвера на  " + gameObject.name);
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

        public void SetPause(bool state)
        {
            if (state)
            {
                attackBehaviour.Pause();
                moveBehaviour.Pause();
            }
            else
            {
                attackBehaviour.UnPause();
                moveBehaviour.UnPause();
            }
        }
    }

    public interface ICanBePaused
    {
        void SetPause(bool state);
    }
}
