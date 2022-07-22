using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class PlayerMovementBehaviour : MovementBehaviour
    {

        internal override void Jump()
        {
            if (!JumpPossible())
                return;
            Debug.Log("Player jump");
            base.Jump();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            CheckGround(collider.transform, true);
        }


        void OnTriggerExit2D(Collider2D collider)
        {
            CheckGround(collider.transform, false);

        }

        private void CheckGround(Transform groundTR, bool isEnter)
        {
            if (groundTR.tag == "ground")
            {
                // Ground 
                if (transform.InverseTransformPoint( groundTR.position).y < 0)
                    IsOnGround = isEnter;
            }
        }

        private bool JumpPossible() => IsOnGround && !isDead;

    }
}