using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class CamGizmoSettings
{
    public bool drawGizmo = true;
    public Color cameraColor = Color.blue;
    
}


public class CameraDetection : MonoBehaviour {

    public CamGizmoSettings gizmoSettings;

    [Tooltip("the center of the object is determined by the collider, as well as the visibility in the camera")]
    public Collider targetCollider;
    public Camera m_camera;
    public UnityEvent m_OnVisible;
    public UnityEvent m_OnVisibleEnter;
    public UnityEvent m_OnVisibleExit;

    private bool isCamDetectionState = false;

    public bool enable = true;
	
	void Update () {
        if (!enable) return;

        if (IsVisibleFrustum(targetCollider.bounds, m_camera))
        {
            m_OnVisible.Invoke();

            if (!isCamDetectionState)
            {
                isCamDetectionState = true;
                m_OnVisibleEnter.Invoke();
            }
        }
        else
        {
            if (isCamDetectionState)
            {
                isCamDetectionState = false;
                m_OnVisibleExit.Invoke();
            }
        }
        
	}

   
    /// <summary>
    /// Determining the visibility of an object in the camera
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    private bool IsVisibleFrustum(Bounds bounds, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, bounds);

    }

    void OnDrawGizmos()
    {
        if (gizmoSettings.drawGizmo)
        {
            Gizmos.color = gizmoSettings.cameraColor;
            Matrix4x4 tempMat = Gizmos.matrix;
            if (m_camera.orthographic)
            {
                Camera c = m_camera;
                var size = c.orthographicSize;
                Gizmos.DrawWireCube(Vector3.forward * (c.nearClipPlane + (c.farClipPlane - c.nearClipPlane) / 2)
                    , new Vector3(size * 2.0f, size * 2.0f * c.aspect, c.farClipPlane - c.nearClipPlane));
            }
            else
            {
                Camera c = m_camera;
                Gizmos.matrix = Matrix4x4.TRS(m_camera.transform.position, m_camera.transform.rotation, Vector3.one);
                Gizmos.DrawFrustum(Vector3.zero, c.fieldOfView, c.farClipPlane, c.nearClipPlane, c.aspect);
            }
            
            Gizmos.matrix = tempMat;
        }



    
    }
}
