using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStorage : MonoBehaviour
{
    public List<Transform> pointList;
    public List<GameObject> products;
    public GameObject currentProduct;
    private GameObject playerObj;
    private Player playerComponent;
    private bool notMoveNow = true;
    private bool playerInZone = false;
    private int i;

    void Start()
    {
        FillPointList();
    }

    public void FillPointList()
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
            i = playerComponent.inventoryProductsList.Count - 1;
            MoveProduct();
        }
        
    }

    private void MoveProduct()
    {
        if(playerInZone && notMoveNow && InputStorageIsNotFull())
        {
            currentProduct = playerComponent.inventoryProductsList[i];
            if(CorrectTypeOfProduct())
            {
                notMoveNow = false;
                StartCoroutine("MoveProductToStorage");
            }
            else
            {
                i--;
                MoveProduct();
            }
        }
    }

    private IEnumerator MoveProductToStorage()
    {
        Vector3 pointPosition = pointList[products.Count].position;
        currentProduct.transform.parent = null;
        while(Vector3.Distance(currentProduct.transform.position, pointPosition) > 0.05)
        {
            currentProduct.transform.position = Vector3.Lerp(currentProduct.transform.position, pointPosition, Time.deltaTime *  15f);
            yield return null;
        }
        currentProduct.transform.rotation = Quaternion.Euler(0, 0, 0);
        products.Add(currentProduct);
        playerComponent.inventoryProductsList.Remove(currentProduct);
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
    public virtual bool CorrectTypeOfProduct()
    {
        if(currentProduct.tag == "BlueCube")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool InputStorageIsNotFull()
    {
        if(products.Count < 15)
        {
            return true;
        }
        else
        {
            Debug.Log("Input Storage is full");
            return false;
        }
    }
}
