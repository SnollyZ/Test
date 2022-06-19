using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenInputStorage : InputStorage
{
    void Start()
    {
        FillPointList();
    }

    public override bool CorrectTypeOfProduct()
    {
        if(currentProduct.tag == "BlueCube" || currentProduct.tag == "RedCube")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
