using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class MovementBehaviour : BaseBehaviour
    {
        [SerializeField]
        protected bool isControllJump;
        [SerializeField]
        protected float speed;
        [SerializeField]
        protected float jumpHeight, jumpSpeed;
        [SerializeField]
        protected SpriteRenderer spriteRenderer;
        // When unit hide from camera
        protected Action OnScreenEndAction;

        protected CapsuleCollider2D capsuleCollider;
        protected MoveState moveState;
        protected Coroutine jumpCoroutine;


        public virtual bool IsOnGround
        {
            get => isOnGround;
            protected set
            {
                isOnGround = value;
            }
        }

        public Action<MoveState> OnMoveStateChange;
        private bool isOnGround = false;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            if(IsOnGround == false)
            {
                StartCoroutine(JumpCorout(false));
            }
        }

        // -1 -> left; +1 -> right; 0 -> stand
        internal void Move(MoveState direction)
        {
            if (isDead)
                return;
            if (!IsOnGround && !isControllJump)
                return;
            // Left or right direction
            Vector3 moveDirect = Vector3.right * (int)direction;

            CheckMoveEnd(moveDirect * Time.deltaTime * speed);

            CheckMoveState(direction);
        }

        protected void CheckMoveEnd(Vector3 nextPos)
        {

            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position + nextPos);
            if (screenPoint.x < 0 || screenPoint.x > 1)
            {
                OnScreenEndAction?.Invoke();
            }
            else
            {
                transform.position += nextPos;
            }
        }

        // Compare current and new movement states and tell others
        protected void CheckMoveState(MoveState newState)
        {
            if(newState != moveState)
            {
                if(IsOnGround)
                    OnMoveStateChange?.Invoke(newState);

                moveState = newState;

                if (newState == MoveState.moveLeft)
                    spriteRenderer.flipX = true;
                else if (newState == MoveState.moveRight)
                    spriteRenderer.flipX = false;

            }

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

        protected IEnumerator MoveUP(float direct, Func<bool> exitPredicat)
        {
            while (!exitPredicat.Invoke())
            {
                Vector3 directVect = Vector3.up * direct * jumpSpeed;
                transform.position += directVect * Time.deltaTime;
                yield return null;
            }
        }

        protected void OnTriggerEnter2D(Collider2D collider)
        {
            CheckGround((BoxCollider2D)collider, true);
        }


        protected void OnTriggerExit2D(Collider2D collider)
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
    }
}