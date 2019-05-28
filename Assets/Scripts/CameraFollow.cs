using UnityEngine;
using System.Collections;
using System.Numerics;
using UnityEngine.SocialPlatforms;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    public Transform targetTransform;
    private Vector3 cameraOffset;

    [Range(0f, 4.0f)] public float smoothTime;
    public bool LookAtTarget = false;
    public bool RotateAroundTarget = false;
    public float RotationSpeed = 5.0f;

    void Start()
    {
        /*Vector3 targetPosition = target.TransformPoint(new Vector3(0, 1, -3));

        transform.position = targetPosition;
        
        transform.LookAt(target);*/

        cameraOffset = transform.position - targetTransform.position;
    }
    void LateUpdate()
    {      
        /*Vector3 targetPosition = target.TransformPoint(new Vector3(0, 1, -3));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        transform.LookAt(target);*/

        if (RotateAroundTarget)
        {
            Quaternion camTurnAngle =
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);

            cameraOffset = camTurnAngle * cameraOffset;
        }

        Vector3 newPos = targetTransform.position + cameraOffset;
        //transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);

        if (LookAtTarget || RotateAroundTarget)
        {
            transform.LookAt(targetTransform);
        }

    }
}
