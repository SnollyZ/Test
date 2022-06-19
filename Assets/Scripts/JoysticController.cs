using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoysticController : MonoBehaviour
{
    public GameObject marker;
    Vector3 targetVector;

    [SerializeField]private GameObject playerObj;
    private Player playerComponent;
    
    void Start()
    {
        playerComponent = playerObj.GetComponent<Player>();
        marker.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 touchPos = Input.mousePosition;
            targetVector = touchPos - transform.position;
            targetVector.z = targetVector.y;
            if(targetVector.magnitude < 110)
            {
                marker.transform.position = touchPos;
                playerComponent.dir = targetVector;
            }
        }
        else
        {
            marker.transform.position = transform.position;
            playerComponent.dir = Vector3.zero;
        }
    }
}
