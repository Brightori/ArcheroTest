using HECS.Controllers;
using System;
using UnityEngine;

namespace Characters
{
    public class PlayerStandartMoveBehaviour : MoveBehaviour<IMovable>
    {
        public PlayerStandartMoveBehaviour(IMovable movable) : base(movable)
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
