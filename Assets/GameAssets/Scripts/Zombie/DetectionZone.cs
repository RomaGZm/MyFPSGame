using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GizmoSettings
{
    public bool drawZone = true;
    public Color zoneColor = Color.blue;
   
    public bool drawRaycast = true;
    public Color rayColor = Color.green;
   
}

public class DetectionZoneEvents: UnityEvent<bool>
{

}

    public class DetectionZone : MonoBehaviour {
    
  
    public GizmoSettings gizmoSettings;

    public Transform target;
    [Tooltip("the center of the object is determined by the collider")]
    public Collider zombieCollider;
    

    [Tooltip("if less than or equal to zero then disabled")]
    public float detectionDistace = 5f;

    public bool isCheckObstacles = false;
    public string obstacleTag = "Wall";
    public Transform[] rayPoints;


    private bool isObstacles = false;
    private bool isZoneDetection = false;
    

    public UnityEvent m_OnZoneDetection = new UnityEvent();
    public UnityEvent m_OnZoneDetectionEnter = new UnityEvent();
    public UnityEvent m_OnZoneDetectionExit = new UnityEvent();

    public bool enable = true;
   
   
    void Start () {

        isZoneDetection = !checkDistace(target.transform.position, detectionDistace);
    }
	
	
	void Update () {
        if (!enable) return;

        if (isCheckObstacles)
        {
            RaycastHit[] hits = rayCastPoints(rayPoints, target.position, gizmoSettings.drawRaycast, gizmoSettings.rayColor);

            foreach (RaycastHit hit in hits)
            {

                if (hit.transform.gameObject.tag == obstacleTag)
                    isObstacles = true;
                else isObstacles = false;
            }
        }
        else isObstacles = false;

        // Debug.Log(isObstacles);
        if (isCheckObstacles && isObstacles) return;
        if (checkDistace(target.transform.position, detectionDistace))
        {
            
            m_OnZoneDetection.Invoke();

            if (isZoneDetection)
            {
                isZoneDetection = false;
                m_OnZoneDetectionEnter.Invoke();
            }
        }
        else
        {
           
            if (!isZoneDetection && !checkDistace(target.transform.position, detectionDistace))
            {
                isZoneDetection = true;
                m_OnZoneDetectionExit.Invoke();
            }
        }
    }




    /// <summary>
    /// The method checks the intersection of the rays to the specified target
    /// </summary>
    /// <param name="startPoints">  Ray start points </param>
    /// <param name="target"> End of ray </param>
    /// <param name="drawRays"> You need to draw rays</param>
    /// <returns></returns>
    private RaycastHit[] rayCastPoints(Transform[] startPoints, Vector3 target, bool drawRays, Color lineColor)
    {
        Ray[] rays = new Ray[startPoints.Length];
        RaycastHit[] hits = new RaycastHit[startPoints.Length];
        int hitsCounter = 0;
        for (int i = 0; i < startPoints.Length; i++)
        {
            Vector3 pos = startPoints[i].position;

            Vector3 dir = (target - startPoints[i].position).normalized;
            float dist = Vector3.Distance(startPoints[i].position, target);
            rays[i] = new Ray(pos, dir * dist);

            if (Physics.Raycast(rays[i], out hits[i], dist))
            {
                hitsCounter++;

            }

            if (drawRays) Debug.DrawRay(pos, dir * dist, lineColor);

        }

        return hits;
    }

    private bool checkDistace(Vector3 target, float distance)
    {
        return Vector3.Distance(zombieCollider.bounds.center, target) <= distance;

    }


    void OnDrawGizmos()
    {
        if (gizmoSettings.drawZone)
        {
            Gizmos.color = gizmoSettings.zoneColor;
            Gizmos.DrawWireSphere(zombieCollider.bounds.center, detectionDistace);

           // Gizmos.color = gizmoSettings.rayColor;
           // Gizmos.DrawLine(GetComponent<Collider>().bounds.center, player.GetComponent<Collider>().bounds.center);

            

        }
    }

   
}
