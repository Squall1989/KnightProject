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

        public virtual bool IsOnGround
        {
            get => isOnGround;
            protected set
            {
                isOnGround = value;
            }
        }

        public Action<MoveState> OnMoveStateChange;
        private bool isOnGround = true;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            capsuleCollider = GetComponent<CapsuleCollider2D>();
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



        protected IEnumerator MoveUP(float direct, Func<bool> exitPredicat)
        {
            while (!exitPredicat.Invoke())
            {
                Vector3 directVect = Vector3.up * direct * jumpSpeed;
                transform.position += directVect * Time.deltaTime;
                yield return null;
            }
        }
    }
}