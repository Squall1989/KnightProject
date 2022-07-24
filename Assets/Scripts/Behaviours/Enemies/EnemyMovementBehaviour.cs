using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class EnemyMovementBehaviour : MovementBehaviour
    {
        protected override void Start()
        {
            base.Start();
            OnScreenEndAction = () =>
            {
                // Return to pool
            };
        }
    }
}