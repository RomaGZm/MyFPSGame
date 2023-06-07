using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Core.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
 
        [Header("State")]
        public bool isRunning = false;
        public bool isGrounded = false;

       
        #region Events
        public event Action<Vector3> playerMove;
        public event Action playerJump;
        #endregion
        
        #region  Animation
        public float animMoveXSmooth = 1;
        public float animMoveYSmooth = 1;
        #endregion

        #region Private
        private CharacterController chController;

        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0;
        private bool canMove = true;
        private float tempMoveX = 0;
        private float tempMoveY = 0;
        Vector2 rotation = Vector2.zero;
        #endregion

        private void Awake()
        {
            chController = GetComponent<CharacterController>();
        }

        void Update()
        {
            isGrounded = chController.isGrounded;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float vert = Input.GetAxis("Vertical");
            float hor = Input.GetAxis("Horizontal");

            //Is running
            isRunning = Input.GetKey(GameManager.Instance.playerData.inpuSettings.KeyRun);
            float curSpeedX = canMove ? (isRunning ? GameManager.Instance.playerData.moveSettings.runSpeed : GameManager.Instance.playerData.moveSettings.walkSpeed) * vert : 0;
            float curSpeedY = canMove ? (isRunning ? GameManager.Instance.playerData.moveSettings.runSpeed : GameManager.Instance.playerData.moveSettings.walkSpeed) * hor : 0;
            float movementDirectionY = moveDirection.y;

            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            //Smooth animation move
            tempMoveX = Mathf.MoveTowards(tempMoveX, hor, Time.deltaTime * animMoveXSmooth);
            tempMoveY = Mathf.MoveTowards(tempMoveY, vert, Time.deltaTime * animMoveYSmooth);
            playerMove.Invoke(new Vector3(tempMoveY, tempMoveX, moveDirection.y));

           

            if (Input.GetKey(GameManager.Instance.playerData.inpuSettings.KeyJump) && canMove && chController.isGrounded)
            {
                moveDirection.y = GameManager.Instance.playerData.moveSettings.jumpSpeed;
                playerJump.Invoke();
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

           

            if (!chController.isGrounded)
            {
                moveDirection.y -= GameManager.Instance.playerData.moveSettings.gravity * Time.deltaTime;
            }

            chController.Move(moveDirection * Time.deltaTime);


            if (canMove)
            {
                 transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * GameManager.Instance.playerData.moveSettings.lookSpeed, 0);
              
            }

        }
      

    }
}