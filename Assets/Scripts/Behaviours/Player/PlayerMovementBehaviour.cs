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

        protected Coroutine jumpCoroutine;

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

        protected IEnumerator JumpCorout(bool up)
        {
            OnMoveStateChange?.Invoke(MoveState.jump);


            float startPosY = transform.position.y;
            // Jump
            if (up)
            {
                yield return MoveUP(1.5f, () => transform.position.y >= startPosY + jumpHeight);
                IsOnGround = false;
            }
            // Fall
            yield return MoveUP(-1f, () => IsOnGround);

            OnMoveStateChange?.Invoke(moveState);

            jumpCoroutine = null;
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            CheckGround((BoxCollider2D)collider, true);
        }


        void OnTriggerExit2D(Collider2D collider)
        {
            CheckGround((BoxCollider2D)collider, false);

        }

        private void CheckGround(BoxCollider2D groundCollider, bool isEnter)
        {
            if (groundCollider.tag == "ground")
            {
                Vector3 inverseVector = transform.InverseTransformPoint(groundCollider.transform.position);

                float colloderPoint = Mathf.Abs(inverseVector.y) - groundCollider.size.y;

                bool standGround = colloderPoint < 0;

                // Ground 
                if (standGround)
                    IsOnGround = isEnter;
            }
        }

        private bool JumpPossible() => IsOnGround && !isDead;

    }
}