using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public GameObject level1Pos, level2Pos, level3Pos;
    public bool isAtLevel1, isAtLevel2, isAtLevel3;
    public GameObject mainCamera;

    public GameObject wallLimitLeft, wallLimitRight;

    private void Update()
    {
        Vector3 curLevelPos = new Vector3(0,0,0);
        
        if (LevelManager.Instance.playerLevel == 1)
        {
            curLevelPos = level1Pos.transform.position;
        }
        else if (LevelManager.Instance.playerLevel == 2)
        {
            curLevelPos = level2Pos.transform.position;
        }
        else if (LevelManager.Instance.playerLevel == 3)
        {
            curLevelPos = level3Pos.transform.position;
        }

        float x = 0;
        if (PlayerController.Instance.transform.position.x > wallLimitLeft.transform.position.x)
        {
            x = PlayerController.Instance.transform.position.x;
        }
        else
        {
            x = wallLimitLeft.transform.position.x;
        }

        if (PlayerController.Instance.transform.position.x < wallLimitRight.transform.position.x)
        {
            x = PlayerController.Instance.transform.position.x;
        }
        else
        {
            x = wallLimitRight.transform.position.x;
        }

        mainCamera.transform.position = new Vector3(x, curLevelPos.y, -10);
    }
}
