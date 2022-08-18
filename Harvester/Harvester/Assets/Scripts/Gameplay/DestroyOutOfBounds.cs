using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

    public float topBorder = 200;
    public float lowerBorder = -55f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > topBorder || transform.position.z < lowerBorder)
        {
            Destroy(gameObject);
        }
    }
}
