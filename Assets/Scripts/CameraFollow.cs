using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 3f;
    private Vector3 velocity = Vector3.zero;
    //public Vector3 offset;

    void Start()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(2, 1, -3));

        transform.position = targetPosition;
        
        transform.LookAt(target);
    }
    void LateUpdate()
    {
        /*Vector3 targetPosition = target.position + offset;

        transform.position = targetPosition;//smoothedPosition;
            
        transform.LookAt(target);*/ 
        
        Vector3 targetPosition = target.TransformPoint(new Vector3(2, 1, -3));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        transform.LookAt(target);
    }
}
