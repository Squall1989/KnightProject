﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class PlayerInput : MonoBehaviour, Initiable<PlayerMovementBehaviour>
    {
        private PlayerMovementBehaviour movementBehaviour;


        public void Init(PlayerMovementBehaviour initType)
        {
            movementBehaviour = initType;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                movementBehaviour.Move(MoveState.moveRight);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                movementBehaviour.Move(MoveState.moveLeft);
            }
            else
            {
                movementBehaviour.Move(MoveState.stand);
            }

            if(Input.GetAxis("Jump") > 0)
            {
                movementBehaviour.Jump();
            }
        }


    }
}