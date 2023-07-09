using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public GameObject lvl1PatrollA, lvl1PatrollB, lvl2PatrollA, lvl2PatrollB, lvl3PatrollA, lvl3PatrollB;
    public float playerLevel;

    public GameObject bossPatrolllvl1AL, bossPatrolllvl1BL, bossPatrolllvl1AR, bossPatrolllvl1BR, bossPatrolllvl2AL, bossPatrolllvl2BL, bossPatrolllvl2AR, bossPatrolllvl2BR;
}
