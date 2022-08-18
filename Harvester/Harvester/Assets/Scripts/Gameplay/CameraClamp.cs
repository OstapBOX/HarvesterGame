using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour {
    [SerializeField]
    private Transform targetToFollow;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed;

    void FixedUpdate() {
        Vector3 desiredPosition = new Vector3(targetToFollow.transform.position.x, 0 , 0) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
