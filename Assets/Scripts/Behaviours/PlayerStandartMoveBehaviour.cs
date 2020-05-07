using Behaviours;
using Components;
using HECS.Controllers;
using UnityEngine;

namespace Behaviours
{
    public class PlayerStandartMoveBehaviour : MoveBehaviour<IMoveAndRotate>
    {
        public PlayerStandartMoveBehaviour(IMoveAndRotate movable) : base(movable)
        {
        }

        public override void Pause()
        {
            state = MoveStates.PAUSE;
        }

        public override void UnPause()
        {
            state = MoveStates.UNPAUSE;
        }

        public override void Update()
        {
            switch (state)
            {
                case MoveStates.DEFAULT:
                    Move();
                    break;
                case MoveStates.PAUSE:
                    break;
                case MoveStates.UNPAUSE:
                    state = MoveStates.DEFAULT;
                    break;
            }
        }

        private void Move()
        {
            movable.Transform.position += new Vector3 (InputController.Instance.HorizontalMove*movable.MoveSpeed*Time.deltaTime, 0, 
                InputController.Instance.VerticalMove*movable.MoveSpeed*Time.deltaTime);
        }
    }
}
