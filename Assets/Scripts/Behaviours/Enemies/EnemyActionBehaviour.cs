using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class EnemyActionBehaviour : ActionBehaviour
    {
        private void OnEnable()
        {
            StartMove();

        }

        private void StartMove()
        {
            Vector3 screenPos = Camera.main.ViewportToScreenPoint(transform.position);
            // If start from left part of screen, moving right
            MoveState startMove = screenPos.x > .5f ? MoveState.moveLeft : MoveState.moveRight;
            currentAction = new RunAction(movementBehaviour, startMove);
        }
    }
}