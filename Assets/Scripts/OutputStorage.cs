using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputStorage : MonoBehaviour
{
    public List<Transform> pointList;
    public List<GameObject> products;
    private GameObject playerObj;
    private GameObject currentProduct;
    private Player playerComponent;
    private bool notMoveNow = true;
    private bool playerInZone = false;

    void Start()
    {
        foreach(Transform child in transform)
        {
            pointList.Add(child);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInZone = true;
            playerObj = other.gameObject;
            playerComponent = playerObj.GetComponent<Player>();
            MoveProduct();
        }
        
    }

    private void MoveProduct()
    {
        if(playerInZone && notMoveNow)
        {
            currentProduct = products[products.Count - 1];
            notMoveNow = false;
            StartCoroutine("MoveProductToInventory");
        }
    }

    private IEnumerator MoveProductToInventory()
    {
        while(true)
        {
            products.Remove(currentProduct);
            Vector3 pointPosition = playerComponent.inventoryPointList[playerComponent.inventoryProductsList.Count].position;
            if(Vector3.Distance(currentProduct.transform.position, pointPosition) > 0.05)
            {
                currentProduct.transform.position = Vector3.Lerp(currentProduct.transform.position, pointPosition, Time.deltaTime *  15f);
                yield return null;
            }
            else
            {
                currentProduct.transform.position = pointPosition;
                currentProduct.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            }
        }
        currentProduct.transform.parent = playerObj.transform.GetChild(0).transform;
        playerComponent.inventoryProductsList.Add(currentProduct);
        notMoveNow = true;
        MoveProduct();
        yield break;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInZone = false;
        }
    }
}
