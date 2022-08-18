using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float leftAngle;
    [SerializeField] private float rightAngle;
    [SerializeField] private float rotationSpeed;
    private float direction = 1;


    private void Update() {
        if(transform.rotation.y > rightAngle) {
            direction = -1;
        }
        if(transform.rotation.y < leftAngle) {
            direction = 1;
        }
        transform.Rotate(0, Time.deltaTime * rotationSpeed * direction, 0, Space.World);
    }   
}
