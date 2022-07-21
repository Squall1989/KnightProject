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

        private void OnTriggerEnter(Collider collider)
        {
            CheckGround(collider.transform, true);
        }

        private void OnTriggerExit(Collider collider)
        {
            CheckGround(collider.transform, false);

        }

        private void CheckGround(Transform groundTR, bool isEnter)
        {
            if (groundTR.tag == "ground")
            {
                // Ground 
                if (groundTR.position.y < transform.position.y)
                    IsOnGround = isEnter;
            }
        }

        private bool JumpPossible() => IsOnGround && !isDead;

    }
}