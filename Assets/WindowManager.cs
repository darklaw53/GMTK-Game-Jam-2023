using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Singleton<WindowManager>
{
    public string request1, request2, request3, request4;
    public Sprite reqSprite1, reqSprite2, reqSprite3, reqSprite4;
    public Transform reqPos1, reqPos2, reqPos3, reqPos4;
    public int currentRequest = 1;

    public GameObject deliveryWindow;
    Vector3 pos1, pos2, pos3, pos4;

    private void Start()
    {
        pos1 = reqPos1.position;
        pos2 = reqPos2.position;
        pos3 = reqPos3.position;
        pos4 = reqPos4.position;
    }

    public void TakeObject(string objectName, GameObject obj)
    {
        if (currentRequest == 1 && objectName == request1)
        {
            deliveryWindow.SetActive(true);
            Destroy(obj);
            currentRequest++;
        }
        else if (currentRequest == 2 && objectName == request2)
        {
            deliveryWindow.SetActive(true);
            Destroy(obj);
            currentRequest++;
        }
        else if (currentRequest == 3 && objectName == request3)
        {
            deliveryWindow.SetActive(true);
            Destroy(obj);
            currentRequest++;
        }
        else if (currentRequest == 4 && objectName == request4)
        {
            deliveryWindow.SetActive(true);
            Destroy(obj);
        }
        else
        {
            if (objectName == request1)
            {
                obj.transform.position = pos1;
            }
            else if (objectName == request2)
            {
                obj.transform.position = pos2;
            }
            else if (objectName == request3)
            {
                obj.transform.position = pos3;
            }
            else if (objectName == request4)
            {
                obj.transform.position = pos4;
            }
        }
    }
}
