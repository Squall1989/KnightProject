using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class PlayerMovementBehaviour : MovementBehaviour
    {
        public override bool IsOnGround {
            get => base.IsOnGround;
            protected set {
                base.IsOnGround = value;

                if (jumpCoroutine == null)
                    jumpCoroutine = StartCoroutine(JumpCorout(false));
            }
        }


        protected override void Start()
        {
            OnScreenEndAction = () =>
            {
                //Move(moveState == MoveState.moveLeft ? MoveState.moveRight : MoveState.moveLeft);
            };
        }

        
        internal  void Jump()
        {
            if (!JumpPossible())
                return;

            if (jumpCoroutine != null)
                return;

            jumpCoroutine = StartCoroutine(JumpCorout(true));
        }

        



        private bool JumpPossible() => IsOnGround && !isDead;

    }
}