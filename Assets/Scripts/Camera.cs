using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    public float smoothSpeed = 0.5f;
    public Vector3 offset;
    public float shiftDistance;
    private Transform target;

    private void Start() 
    {
        target = GameObject.Find("Player").transform;
    }

    private void FixedUpdate() 
    {
        if(target != null){
            Vector3 desiredPosition = target.position + offset + new Vector3(Input.GetAxis("Horizontal") * shiftDistance, 0 , Input.GetAxis("Vertical") * shiftDistance);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
        else
            Debug.Log("Camera: Can't find player");
    }
}
