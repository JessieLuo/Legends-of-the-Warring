using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the camera follow the target which is set to the first player.
/// Can rotate camera to another target on next turn. (must be implemented)
/// </summary>
public class CameraMoving : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;

    void LateUpdate()
    {
        transform.position = target.position + cameraOffset;
    }
}
