using System.Collections;
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

            playerInput.Init(FindBehaviourOfType(typeof(MovementBehaviour)) as MovementBehaviour);
        }
    }
}