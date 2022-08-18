using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 finalPosition;
    private float rotationSpeed = 70;

    private Vector3 initialPosition;
  

    private void Awake() {
        initialPosition = transform.position;
    }

    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, finalPosition, 0.1f);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnDisable() {
        transform.position = initialPosition;
    }
}
