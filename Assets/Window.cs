using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : Singleton<Window>
{
    public GameObject thoughtBubble;
    public SpriteRenderer thoughtBubbleObject;

    public GameObject itemBase;
    bool inFrontOFWindow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        thoughtBubble.SetActive(true);
        inFrontOFWindow = true;
    }

    public void UpdateRequest(List<Items> items)
    {
        thoughtBubble.SetActive(true);
     
        foreach (Transform child in thoughtBubbleObject.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Items child in items)
        {
            var x = Instantiate(itemBase, thoughtBubbleObject.transform);
            x.GetComponent<SpriteRenderer>().sprite = child.sprite;
        }

        if (items.Count == 1)
        {
            thoughtBubbleObject.transform.position = new Vector3(-4.21f, thoughtBubbleObject.transform.position.y, thoughtBubbleObject.transform.position.z);
        }
        else if (items.Count == 2)
        {
            thoughtBubbleObject.transform.position = new Vector3(-5.1f, thoughtBubbleObject.transform.position.y, thoughtBubbleObject.transform.position.z);
        }
        else if (items.Count == 3)
        {
            thoughtBubbleObject.transform.position = new Vector3(-6f, thoughtBubbleObject.transform.position.y, thoughtBubbleObject.transform.position.z);
        }

        if (!inFrontOFWindow) thoughtBubble.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        thoughtBubble.SetActive(false);
        inFrontOFWindow = false;
    }
}
