using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenFactory : Factory
{
    private GameObject inputStorage;
    private GameObject currentRecource1;
    private GameObject currentRecource2;
    private InputStorage inputStorageComponent;

    void Start()
    {
        inputStorage = transform.GetChild(1).gameObject;
        inputStorageComponent = inputStorage.GetComponent<InputStorage>();
        notification = notificationObj.GetComponent<Notification>();
        Produce();
    }

    public override bool ResourcesAvailable()
    {
        if(inputStorageComponent.products.Count > 0 && RedAndBlueAvailable())
        {
            if(notification.factory == gameObject)
            {
                notification.RevokeNotification();
            }
            return true;
        }
        else
        {
            notification.CallNotification(1, gameObject);
            return false;
        }
    }

    public override IEnumerator MoveRecourceToFactory()
    {   
        Vector3 pointPosition = transform.position;
        while(Vector3.Distance(currentRecource1.transform.position, pointPosition) > 0.05 && Vector3.Distance(currentRecource2.transform.position, pointPosition) > 0.05)
        {
            currentRecource1.transform.position = Vector3.Lerp(currentRecource1.transform.position, pointPosition, Time.deltaTime *  15f);
            currentRecource2.transform.position = Vector3.Lerp(currentRecource2.transform.position, pointPosition, Time.deltaTime *  15f);
            yield return null;
        }
        inputStorageComponent.products.Remove(currentRecource1);
        inputStorageComponent.products.Remove(currentRecource2);
        Destroy(currentRecource1);
        Destroy(currentRecource2);
        yield return new WaitForSeconds(3f);
        StartCoroutine("MoveProductToStorage");
        yield break;
    }

    private bool RedAndBlueAvailable()
    {
        
        for(int i = 1; i <= inputStorageComponent.products.Count; i++)
        {
            if(inputStorageComponent.products[inputStorageComponent.products.Count - i].tag == "BlueCube")
            {
                currentRecource1 = inputStorageComponent.products[inputStorageComponent.products.Count - i];
            }
            else if(inputStorageComponent.products[inputStorageComponent.products.Count - i].tag == "RedCube")
            {
                currentRecource2 = inputStorageComponent.products[inputStorageComponent.products.Count - i];
            }
        }
        if(currentRecource1 != null && currentRecource2 != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
