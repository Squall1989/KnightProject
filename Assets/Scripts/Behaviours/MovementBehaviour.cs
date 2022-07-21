using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class MovementBehaviour : BaseBehaviour
    {
        [SerializeField]
        protected float speed;
        [SerializeField]
        protected float jumpHeight, jumpSpeed;
        [SerializeField]
        protected SpriteRenderer spriteRenderer;

        protected MoveState moveState;
        public bool IsOnGround
        {
            get => isOnGround;
            protected set
            {
                Debug.Log("isOnGround: " + value);
                isOnGround = value;

            }
        }

        public Action<MoveState> OnMoveStateChange;
        private bool isOnGround = true;

        // Start is called before the first frame update
        protected void Start()
        {

        }

        // -1 -> left; +1 -> right; 0 -> stand
        internal void Move(MoveState direction)
        {
            if (isDead || !IsOnGround)
                return;

            // Left or right direction
            Vector3 moveDirect = Vector3.right * (int)direction;
            transform.position += moveDirect * Time.deltaTime * speed;

            CheckMoveState(direction);
        }

        // Compare current and new movement states and tell others
        protected void CheckMoveState(MoveState newState)
        {
            if(newState != moveState)
            {
                OnMoveStateChange?.Invoke(newState);
                moveState = newState;

                if (newState == MoveState.moveLeft)
                    spriteRenderer.flipX = true;
                else if (newState == MoveState.moveRight)
                    spriteRenderer.flipX = false;

            }
        }

        internal virtual void Jump()
        {
            IsOnGround = false;
            StartCoroutine(JumpCorout());
        }

        protected IEnumerator JumpCorout()
        {
            OnMoveStateChange?.Invoke(MoveState.jump);


            float startPosY = transform.position.y;
            // Jump
            yield return MoveUP(1, () => transform.position.y < startPosY + jumpHeight);
            // Fall
            yield return MoveUP(-1, () => IsOnGround);

            OnMoveStateChange?.Invoke(MoveState.stand);

        }

        protected IEnumerator MoveUP(int direct, Func<bool> exitPredicat)
        {
            Debug.Log("MoveUP: " + direct);

            while (!exitPredicat.Invoke())
            {
                Vector3 directVect = Vector3.up * direct * jumpSpeed;
                transform.position += directVect * Time.deltaTime;
                yield return null;
            }
        }
    }
}