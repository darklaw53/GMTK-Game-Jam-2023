using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTail : MonoBehaviour
{
    public EventElement element;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "bar")
        {
            element.ExitBar();
        }
    }
}
