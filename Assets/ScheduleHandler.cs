using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleHandler : Singleton<ScheduleHandler>
{
    public List<GameObject> scheduleList;
    public float scheduleSpeed;
    public Transform eventBar;

    public GameObject lastEvent;

    private void Start()
    {
        foreach (GameObject obj in scheduleList)
        {
            if (lastEvent == null)
            {
                Instantiate(obj, eventBar.transform);
            }
            else
            {
                Instantiate(obj, lastEvent.transform.GetChild(0).position, eventBar.rotation, eventBar.transform);
            }

            lastEvent = obj;
        }
    }

    public void updateAllowedRooms(int roomCode)
    {

    }
}
