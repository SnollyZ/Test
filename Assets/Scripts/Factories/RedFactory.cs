using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFactory : Factory
{
    private GameObject inputStorage;
    private GameObject currentRecource;
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
        if(inputStorageComponent.products.Count > 0)
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
        currentRecource = inputStorageComponent.products[inputStorageComponent.products.Count - 1];
        Vector3 pointPosition = transform.position;
        while(Vector3.Distance(currentRecource.transform.position, pointPosition) > 0.05)
        {
            currentRecource.transform.position = Vector3.Lerp(currentRecource.transform.position, pointPosition, Time.deltaTime *  15f);
            yield return null;
        }
        inputStorageComponent.products.Remove(currentRecource);
        Destroy(currentRecource);
        yield return new WaitForSeconds(3f);
        StartCoroutine("MoveProductToStorage");
        yield break;
    }

}
