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

        protected MoveState moveState;

        public Action<MoveState> OnMoveStateChange;

        // Start is called before the first frame update
        protected void Start()
        {

        }

        // -1 -> left; +1 -> right; 0 - stand
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
            }
        }

    }
}