using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Zombie
{
    [System.Serializable]
    public class MoveSettings
    {
        //[Tooltip("if less than or equal to zero then disabled")]
        public float attackDistace = 1f;
        [Tooltip("The speed of switching between animations")]
        public float linearSpeedStart = 1;
        public float linearSpeedStop = 1;
        public float moveSpeed = 1;
    }

    [RequireComponent(typeof(NavMeshAgent), typeof(ZombieAnimations))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZombieController : MonoBehaviour
    {

        [Header("Components")]
        public MoveSettings moveSettings;
        public GameObject target;
        [HideInInspector]
        public NavMeshAgent agent;
        [SerializeField]
        private ZombieHealth zombieHealth;

        [Header("State")]
        public bool isDie = false;
        private MotionController motionController;

        [Header("Detection")]
        [SerializeField]
        private DetectionZone zoneHear;
        [SerializeField]
        private DetectionZone zoneVisible;
        [SerializeField]
        private CameraDetection cameraDetection;

        private Animator animator;

        private ZombieAnimations zombieAnimations;
        
        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            zombieAnimations = GetComponent<ZombieAnimations>();
            motionController = GetComponent<MotionController>();
            agent.updatePosition = false;

        }

        private void DisableDetection()
        {
            zoneHear.enable = false;
            zoneVisible.enable = false;
            cameraDetection.enable = false;
        }

        void OnAnimatorMove()
        {
         
            transform.position = agent.nextPosition;
        }


        void Update()
        {
            if (agent.remainingDistance < 1)
            {
                Stop();
            }
          

        }
        public void TakeDamage(int damage)
        {
            zombieHealth.health -= damage;
            if (zombieHealth.health <= 0)
            {
               // motionController.StopMotion();
                zombieAnimations.Die();
                zombieHealth.DisableHealtInfo();
                DisableDetection();
                Stop();
                isDie = true;
            }
            else
            {
              
                zombieAnimations.Hit();
            }

        }
        public void Run(Vector3 point)
        {
            agent.isStopped = false;
            zombieAnimations.Run(true);
            agent.destination = point;
        }
        public void Walk(Vector3 point)
        {
            agent.isStopped = false;
            zombieAnimations.Walk(true);
            agent.destination = point;
        }
        public void Stop()
        {

            agent.isStopped = true;
            zombieAnimations.Idle();
        }

        public void OnZoneHear()
        {

        }

        public void OnZoneHearEnter()
        {
          // motionController.StopMotion();
            Debug.Log("OnZoneHearEnter");
        }

        public void OnZoneHearExit()
        {
            //   motionController.StartMotion();
            Stop();
        }

        public void OnZoneVisible()
        {
            Run(target.transform.position);
        }

        public void OnZoneVisibleEnter()
        {
            Run(target.transform.position);
            Debug.Log("OnZoneVisibleEnter");
        }

        public void OnZoneVisibleExit()
        {
          //  motionController.StartMotion();
        }

        public void OnVisibleCamera()
        {
            Run(target.transform.position);
        }

        public void OnVisibleCameraEnter()
        {
           // Run(target.transform.position);
            Debug.Log("OnVisibleCameraEnter");

        }
        public void OnVisibleCameraExit()
        {
           
            // motionController.StartMotion();
        }

        private IEnumerator TimeOutHear(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
        private void ZombieMove()
        {

            float dist = Vector3.Distance(GetComponent<Collider>().bounds.center, target.transform.position);

            if (dist != Mathf.Infinity && agent.remainingDistance < 2.5f)
            {
                agent.isStopped = true;
                float tempMove = Mathf.Lerp(animator.GetFloat("Move"), 0, Time.deltaTime * moveSettings.linearSpeedStop);
                animator.SetFloat("Move", tempMove);
            }
            else
            {
                agent.isStopped = false;

                float tempMove = Mathf.Lerp(animator.GetFloat("Move"), 1, Time.deltaTime * moveSettings.linearSpeedStart);
                animator.SetFloat("Move", tempMove);
            }


        }
    }

}
