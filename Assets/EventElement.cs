using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventElement : MonoBehaviour
{
    public EventTail tail;
    public TextMeshProUGUI title;
    public string textText;
    public int targetRoom;

    float speed;

    private void Start()
    {
        title.text = textText;
        speed = ScheduleHandler.Instance.scheduleSpeed;
    }

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "needle")
        {
            TouchNeedle();
        }
    }

    void TouchNeedle()
    {
        ScheduleHandler.Instance.updateAllowedRooms(targetRoom);
    }

    public void ExitBar()
    {
        transform.position = ScheduleHandler.Instance.lastEvent.transform.GetChild(0).transform.position;
    }
}
