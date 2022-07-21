using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class AnimationBehaviour : BaseBehaviour, Initiable<MovementBehaviour>
    {
        [SerializeField]
        Animator animatorController;

        private MovementBehaviour movementBehaviour;

        public void Init(MovementBehaviour initType)
        {
            movementBehaviour = initType;

            movementBehaviour.OnMoveStateChange += MoveAnimChange;
        }

        private void MoveAnimChange(MoveState moveState)
        {
            switch(moveState)
            {
                case MoveState.jump:
                    animatorController.SetBool(AnimStates.Jump, true);
                    animatorController.SetBool(AnimStates.Move, false);
                    break;
                case MoveState.stand:
                    animatorController.SetBool(AnimStates.Jump, false);
                    animatorController.SetBool(AnimStates.Move, false);
                    break;

                case MoveState.moveLeft:
                case MoveState.moveRight:
                    animatorController.SetBool(AnimStates.Move, true);
                    break;
            }
        }



        // Start is called before the first frame update
        void Start()
        {
            if(animatorController == null)
            {
                Debug.LogError("Animator controller not set for " + gameObject.name);
            }
        }


    }
}