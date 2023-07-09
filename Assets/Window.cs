using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject thoughtBubble;
    public SpriteRenderer thoughtBubbleObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        thoughtBubble.SetActive(true);
    }

    private void Update()
    {
        if (WindowManager.Instance.currentRequest == 1)
        {
            thoughtBubbleObject.sprite = WindowManager.Instance.reqSprite1;
        }
        else if (WindowManager.Instance.currentRequest == 2)
        {
            thoughtBubbleObject.sprite = WindowManager.Instance.reqSprite2;
        }
        else if (WindowManager.Instance.currentRequest == 3)
        {
            thoughtBubbleObject.sprite = WindowManager.Instance.reqSprite3;
        }
        else if (WindowManager.Instance.currentRequest == 4)
        {
            thoughtBubbleObject.sprite = WindowManager.Instance.reqSprite4;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        thoughtBubble.SetActive(false);
    }
}
