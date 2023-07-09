using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escapemanager : Singleton<escapemanager>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
