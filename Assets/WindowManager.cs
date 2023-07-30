using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Singleton<WindowManager>
{
    public string request1, request2, request3, request4;
    public Sprite reqSprite1, reqSprite2, reqSprite3, reqSprite4;
    public Transform reqPos1, reqPos2, reqPos3, reqPos4;
    public string currentRequdest;

    public GameObject deliveryWindow;
    public Vector3 pos1, pos2, pos3, pos4;

    public List<RequestSO> allRequests;
    List<Items> currentRequest;

    private void Start()
    {
        pos1 = reqPos1.position;
        pos2 = reqPos2.position;
        pos3 = reqPos3.position;
        pos4 = reqPos4.position;

        NewRequest();
    }

    public void NewRequest()
    {
        if (allRequests[0] != null)
        {
            currentRequest = allRequests[0].itemList;
            Window.Instance.UpdateRequest(currentRequest);

            var y = new List<RequestSO>();
            foreach (RequestSO x in allRequests)
            {
                if (x.itemList != currentRequest)
                {
                    y.Add(x);
                }
            }
            allRequests = y;
        }
    }

    public void TakeObject(string objectName, GameObject obj)
    {
        foreach (Items item in currentRequest)
        {
            if (item.itemName == objectName)
            {
                var y = new List<Items>();
                foreach (Items x in currentRequest)
                {
                    if (x != item)
                    {
                        y.Add(x);
                    }
                }
                currentRequest = y;

                if (y.Count <= 0)
                {
                    deliveryWindow.SetActive(true);
                    NewRequest();
                }
                else
                {
                    Window.Instance.UpdateRequest(currentRequest);
                }

                PlayerController.Instance.heldItem.transform.parent = null;
                PlayerController.Instance.heldItem = null;
                break;
            }
        }

        if (objectName == request1)
        {
            obj.transform.position = pos4;
        }
        else if (objectName == request2)
        {
            obj.transform.position = pos2;
        }
        else if (objectName == request3)
        {
            obj.transform.position = pos1;
        }
        else if (objectName == request4)
        {
            obj.transform.position = pos3;
        }
    }
}