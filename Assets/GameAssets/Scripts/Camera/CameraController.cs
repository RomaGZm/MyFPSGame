using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Cameras 
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        [Header("Rotate")]
        [Range(0.1f, 5f)]
        public float mouseRotateSpeed = 0.8f;
        public float offsetRotX = 0f;
        public float offsetRotY = 0f;
        [Header("Position")]
        public Vector3 offsetPosition;
        [Header("Limit")]
        public float minXRotAngle = -80; //min angle around x axis
        public float maxXRotAngle = 80; // max angle around x axis

        [Header("Other")]
        public float slerpValue = 0.25f;

        private Quaternion cameraRot;
        private float disCamera;
        private float rotX;


        void Start()
        {
            disCamera = Vector3.Distance(transform.position, target.position);

        }


        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButton(1))
            {
                rotX += -Input.GetAxis("Mouse Y") * mouseRotateSpeed;

            }

            if (rotX < minXRotAngle)
            {
                rotX = minXRotAngle;
            }
            else if (rotX > maxXRotAngle)
            {
                rotX = maxXRotAngle;
            }

        }

        private void LateUpdate()
        {
        
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Vector3 dir = new Vector3(0, 0, -disCamera);

            Quaternion newQ;
            newQ = Quaternion.Euler(rotX + offsetRotX, target.transform.rotation.eulerAngles.y + offsetRotY, 0);


            cameraRot = Quaternion.Slerp(cameraRot, newQ, slerpValue);
            transform.position = (target.position + cameraRot * dir);
            transform.LookAt(target.position+offsetPosition);

        }

        public void SetCamPos()
        {

            transform.position = new Vector3(0, 0, -disCamera);
        }

    }


}


