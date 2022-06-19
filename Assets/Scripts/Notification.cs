using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    private Text textComponent;
    private RectTransform rectTransform;
    private bool notMoveNow = true;
    public GameObject factory;

    void Awake()
    {
        textComponent = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
    }


    private IEnumerator MoveText(int notificationType)
    {
        float dir;
        if(notificationType == 1)
        {
            dir = 875f;
            while(rectTransform.localPosition.x > dir + 1f)
            {
                float yVelocity = 0.0f;
                float newPosition = Mathf.SmoothDamp(rectTransform.localPosition.x, dir, ref yVelocity, 0.1f);
                rectTransform.localPosition = new Vector3(newPosition, rectTransform.localPosition.y, rectTransform.localPosition.z);
                yield return null;
            }
        }
        else if(notificationType == 2)
        {
            dir = 1710f;
            while(rectTransform.localPosition.x < dir - 1f)
            {
                float yVelocity = 0.0f;
                float newPosition = Mathf.SmoothDamp(rectTransform.localPosition.x, dir, ref yVelocity, 0.1f);
                rectTransform.localPosition = new Vector3(newPosition, rectTransform.localPosition.y, rectTransform.localPosition.z);
                yield return null;
            }
        }
        notMoveNow = true;
        yield break;
    }

    public void CallNotification(int notificationType, GameObject obj)
    {   
        if(notMoveNow == true && factory == null)
        {
            factory = obj;
            if(notificationType == 1)
            {
                textComponent.text = "На фабрике " + factory.name + " не хватает ресурсов!";
            }
            else if(notificationType == 2)
            {
                textComponent.text = "На фабрике " + factory.name + " закончилось место на складе!";
            }
            notMoveNow = false;
            StartCoroutine(MoveText(1));
        }
    }

    public void RevokeNotification()
    {
        if(notMoveNow == true)
        {
            factory = null;
            notMoveNow = false;
            StartCoroutine(MoveText(2));
        }
    }

}
