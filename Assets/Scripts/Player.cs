using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
   
    public float speed;
    public List<Transform> inventoryPointList;
    public List<GameObject> inventoryProductsList;
    public Vector3 dir = Vector3.zero;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        foreach(Transform child in transform.GetChild(0))
        {
            inventoryPointList.Add(child);
        }
    }

    void Update()
    {
        PlayerMovementController();
    }

    private void PlayerMovementController()
    {
        dir = new Vector3(dir.x * speed, 0 , dir.z * speed);
        rigidBody.velocity = dir;
        transform.rotation = Quaternion.LookRotation(dir);
    }
    


}
