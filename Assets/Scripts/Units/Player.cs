using System;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class Player : Unit
    {
        [SerializeField]
        private PlayerInput playerInput;

        protected override void SetInitiables()
        {
            base.SetInitiables();

            var moveBehaviour = FindBehaviourOfType(typeof( MovementBehaviour));

            playerInput.Init((MovementBehaviour)moveBehaviour);
        }

    }
}