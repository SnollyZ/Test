using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField]private GameObject product;
    private GameObject currentProduct;
    private bool notProducedNow = true;
    private bool FactoryProduce;
    private int productsInStorage = 0;
    public GameObject notificationObj;
    public Notification notification;
    public GameObject outputStorage;
    public OutputStorage outputStorageComponent;

    void Start()
    {
        notification = notificationObj.GetComponent<Notification>();
        Produce();
    }


    public virtual void Produce()
    {   
        
        if(ResourcesAvailable() && OutputStorageIsNotFull() && notProducedNow)
        {
            notProducedNow = false;
            StartCoroutine("MoveRecourceToFactory");
        }
        else
        {
            StartCoroutine("Waiting");
        }
    }

    public virtual bool ResourcesAvailable()
    {
        return true;
    }

    public virtual IEnumerator MoveRecourceToFactory()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine("MoveProductToStorage");
        yield break;
    }

    private bool OutputStorageIsNotFull()
    {
        outputStorage = this.transform.GetChild(0).gameObject;
        outputStorageComponent = outputStorage.GetComponent<OutputStorage>();
        if(outputStorageComponent.products.Count < 15)
        {
            if(notification.factory == this.gameObject)
            {
                notification.RevokeNotification();
            }
            return true;
            
        }
        else
        {
            notification.CallNotification(2, gameObject);
            return false;
        }
    }

    private IEnumerator MoveProductToStorage()
    {
        currentProduct = Instantiate(product, transform.position, Quaternion.identity);
        Vector3 pointPosition = outputStorageComponent.pointList[outputStorageComponent.products.Count].position;
        while(Vector3.Distance(currentProduct.transform.position, pointPosition) > 0.05)
        {
            currentProduct.transform.position = Vector3.Lerp(currentProduct.transform.position, pointPosition, Time.deltaTime *  15f);
            yield return null;
        }
        outputStorageComponent.products.Add(currentProduct);
        notProducedNow = true;
        Produce();
        yield break;
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1f);
        Produce();
        yield break;
    }

   

}
