using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Zombie;

namespace Core.Zombie
{

    [System.Serializable]
    public class MotionPoint
    {
        public Transform point;
        public float startTime = 0;
        public float delayTime = 0;

    }


    public class MotionController : MonoBehaviour
    {
        public MotionPoint[] points;
        [SerializeField]
        private ZombieController zombieController;
        public bool isCycle = true;

        void Start()
        {
            StartCoroutine(Move());
        }

        public void StopMotion()
        {
            isCycle = false;
            StopCoroutine(Move());
            zombieController.Stop();
        }
        public void StartMotion()
        {
            isCycle = true;
            StartCoroutine(Move());
        }
        IEnumerator Move()
        {
            while (isCycle)
            {
                foreach (MotionPoint mp in points)
                {
                    if (zombieController.isDie)
                        break;
                    zombieController.Walk(mp.point.position);

                    yield return new WaitUntil(() => zombieController.agent.isStopped);
                    
                    yield return new WaitForSeconds(mp.delayTime);

                }
            }

        }
    }
}
