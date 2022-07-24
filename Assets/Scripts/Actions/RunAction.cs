using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightProject
{
    public class RunAction : BaseAction
    {
        private MovementBehaviour movementBehaviour;
        private bool stopRun;
        private MoveState moveDirection;

        public RunAction(MovementBehaviour movementBehaviour, MoveState move)
        {
            moveDirection = move;
            this.movementBehaviour = movementBehaviour;
            StartAction();
        }

        public override void StartAction()
        {
            stopRun = false;
            StartRun();
        }

        private async void StartRun()
        {
            while(!stopRun)
            {
                movementBehaviour.Move(moveDirection);

                await Task.Yield();
            }
        }

        public override void StopAction()
        {
            stopRun = true;
        }
    }
}