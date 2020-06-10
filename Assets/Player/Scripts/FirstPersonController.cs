﻿using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

namespace Player
{
    public class FirstPersonController : MonoBehaviour
    {
        #region Variabile

        private CharacterController controller;
        //private Animator animator;

        private int velocityX = Animator.StringToHash("velocityX");
        private int velocityY = Animator.StringToHash("velocityY");

        public float runSpeed = 20f;
        public float walkSpeed = 12f;
        [HideInInspector] public float currentSpeed;

        [HideInInspector] public bool isRunning = false;
        [HideInInspector] public bool isWalking = false;
        [HideInInspector] public bool isIdle = false;
    
        private float gravity = -9.81f;
        private Vector3 gravityVector = Vector3.zero;

        private readonly float _speedChangeSmoothness = 0.25f;
        private readonly float _velocitySmoothness = 0.45f;
        private Vector2 velocity = Vector2.zero;

        #endregion

        private void Start()
        {
            isIdle = true;
            currentSpeed = walkSpeed;
            
            controller = GetComponent<CharacterController>();
            //animator = GameObject.Find("Joe").GetComponent<Animator>();
        }
        
        private void Update()
        {
            ChangeSpeed();
            
            StateChange();
            
            Move();

            GravityAction();
            
            //ModelAnimation();
            
        }

        #region Private Methods

        private void GravityAction()
        {
            if (controller.isGrounded)
            {
                gravityVector = Vector3.zero;
                return;
            }
    
            gravityVector.y += gravity * Time.deltaTime * Time.deltaTime;
            controller.Move(gravityVector);
        }

        private void ChangeSpeed()
        {
            currentSpeed = Input.GetKey(KeyCode.LeftShift)
                ? Mathf.Lerp(currentSpeed, runSpeed, _speedChangeSmoothness)
                : InterpolateFromMaxToMin(currentSpeed, walkSpeed, _speedChangeSmoothness);
        }

        private void StateChange()
        {
            //sprint
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRunning = true;
                isWalking = false;
                return;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRunning = false;
                return;
            }
            
            //plimbat
            var _movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            isWalking = _movementInput != Vector2.zero && !isRunning;

            //idle
            isIdle = !isRunning && !isWalking;
            
        }

        private void Move()
        {
            var _movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            velocity = Vector2.Lerp(velocity, _movementInput, _velocitySmoothness);

            var _desiredMoveDirection = currentSpeed * Time.deltaTime * (velocity.x * transform.right + velocity.y * transform.forward);
            if (!controller.isGrounded) _desiredMoveDirection *= 0.5f;
            controller.Move(_desiredMoveDirection);
        }

        /*
        private void ModelAnimation()
        {
            animator.SetFloat(velocityX, velocity.x);
            animator.SetFloat(velocityY, velocity.y);
        }*/

        #endregion

        #region Help Methods

        private float InterpolateFromMaxToMin(float a, float b, float t)
        {
            var add = (b - a) * t;
            var tmp = a + add;

            return tmp < b ? b : tmp;
        }

        #endregion
        
    }
}
