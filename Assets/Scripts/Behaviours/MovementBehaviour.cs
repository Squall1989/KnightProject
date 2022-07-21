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
        protected SpriteRenderer spriteRenderer;

        protected MoveState moveState;
        protected bool isOnGround;

        public Action<MoveState> OnMoveStateChange;

        // Start is called before the first frame update
        protected void Start()
        {

        }

        // -1 -> left; +1 -> right; 0 -> stand
        internal void Move(MoveState direction)
        {
            if (isDead)
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

        }
    }
}