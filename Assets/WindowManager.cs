using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Singleton<WindowManager>
{
    public string request1, request2, request3, request4;
    int currentRequest = 1;

    public GameObject deliveryWindow;

    public void TakeObject(string objectName)
    {
        if (currentRequest == 1 && objectName == request1)
        {
            deliveryWindow.SetActive(true);
            currentRequest++;
        }
        else if (currentRequest == 2 && objectName == request2)
        {
            deliveryWindow.SetActive(true);
            currentRequest++;
        }
        else if (currentRequest == 3 && objectName == request3)
        {
            deliveryWindow.SetActive(true);
            currentRequest++;
        }
        else if (currentRequest == 4 && objectName == request4)
        {
            deliveryWindow.SetActive(true);
        }
        else
        {
            //bird is annoyed
            PlayerController.Instance.sendingItem = false;
        }
    }
}
