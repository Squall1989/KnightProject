using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public abstract class ActionBehaviour : BaseBehaviour, IKillable, Initiable<MovementBehaviour, AttackBehaviour>
    {
        protected BaseAction currentAction;

        protected MovementBehaviour movementBehaviour;
        protected AttackBehaviour attackBehaviour;

        internal virtual void SetAction(BaseAction newAction)
        {
            currentAction?.StopAction();

            newAction.StartAction();
            currentAction = newAction;
        }

        public override void Kill()
        {
            base.Kill();
            currentAction?.StopAction();
        }


        public void Init(MovementBehaviour initT, AttackBehaviour initU)
        {
            this.movementBehaviour = initT;
            this.attackBehaviour = initU;
        }
    }
}