using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject pointA, pointB;
    public GameObject sightRange, alertSightRange, eyes;
    Rigidbody2D rb2D;
    public Transform currentPoint;

    public float speed;
    float startSpeed;

    SpriteRenderer sprite;
    Color startColor;

    public BoolSO playerIsStealthed;
    public bool whereTheyShouldBe;
    public float patience;
    public bool searchingForPlayer, caughtPlayer;

    GameObject player;
    public float MinDist, MaxDist;
    float zAxis, yAxis;
    bool seesPlayer;

    public float currentLevel;
    public bool isInRoom1, isInRoom2, isInRoom3, isInRoom4, isInRoom5;
    public float destinationTarget = 0;
    bool seekingDestination;
    bool wantsToGoUp, wantsToGoDown;

    public bool isTheBoss;
    bool facingRight = true;
    public Animator anim;

    public bool comesFromTheBottom;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        //anim.SetBool("isRunning", true);
        sprite = GetComponent<SpriteRenderer>();

        //startColor = sprite.color;
        startSpeed = speed;

        player = PlayerController.Instance.gameObject;
        zAxis = transform.position.z;
        yAxis = transform.position.y;
    }

    private void Update()
    {
        GoToTargetDestination(destinationTarget);

        if (rb2D.velocity.x != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        var hit = Physics2D.Linecast(eyes.transform.position, sightRange.transform.position, 1 << LayerMask.NameToLayer("Player"));
        if (searchingForPlayer)
        {
            hit = Physics2D.Linecast(eyes.transform.position, alertSightRange.transform.position, 1 << LayerMask.NameToLayer("Player"));
        }

        if (hit.collider != null && !playerIsStealthed.boolSO && !caughtPlayer && !RoomManager.Instance.isInCorrectRoom)
        {
            if (!searchingForPlayer)
            {
                StartCoroutine("SurprisedToSeePlayer");
            }
            StopCoroutine("CantSeePlayer");
            searchingForPlayer = true;
            seesPlayer = true;
        }
        else
        {
            StartCoroutine("CantSeePlayer");
            seesPlayer = false;
        }

        if (searchingForPlayer)
        {
            //sprite.color = Color.red;
        }
        else
        {
            //sprite.color = startColor;
            speed = startSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (!isTheBoss)
        {/*
            if (currentLevel == 1)
            {
                if (currentPoint == pointA.transform && pointA != LevelManager.Instance.lvl1PatrollA)
                {
                    pointA = LevelManager.Instance.lvl1PatrollA;
                    currentPoint = pointA.transform;
                }
                else if (currentPoint == pointB.transform && pointB != LevelManager.Instance.lvl1PatrollB)
                {
                    pointB = LevelManager.Instance.lvl1PatrollB;
                    currentPoint = pointB.transform;
                }
            }
            else if (currentLevel == 2)
            {
                if (currentPoint == pointA.transform && pointA != LevelManager.Instance.lvl2PatrollA)
                {
                    pointA = LevelManager.Instance.lvl2PatrollA;
                    currentPoint = pointA.transform;
                }
                else if (currentPoint == pointB.transform && pointB != LevelManager.Instance.lvl2PatrollB)
                {
                    pointB = LevelManager.Instance.lvl2PatrollB;
                    currentPoint = pointB.transform;
                }
            }
            else if (currentLevel == 3)
            {
                if (currentPoint == pointA.transform && pointA != LevelManager.Instance.lvl3PatrollA)
                {
                    pointA = LevelManager.Instance.lvl3PatrollA;
                    currentPoint = pointA.transform;
                }
                else if (currentPoint == pointB.transform && pointB != LevelManager.Instance.lvl3PatrollB)
                {
                    pointB = LevelManager.Instance.lvl3PatrollB;
                    currentPoint = pointB.transform;
                }
            }*/
        }

        if (!searchingForPlayer)
        {
            MoveAround();
        }
        else
        {
            ChasePLayer();
        }
    }

    IEnumerator CantSeePlayer()
    {
        yield return new WaitForSeconds(patience);
        searchingForPlayer = false;
    }

    IEnumerator SurprisedToSeePlayer()
    {
        speed = 0;
        rb2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        if (seesPlayer)
        {
            speed = startSpeed;
        }
        else
        {
            yield return new WaitForSeconds(1);
            if (seesPlayer)
            {
                speed = startSpeed;
            }
            else
            {
                StopCoroutine("CantSeePlayer");
                searchingForPlayer = false;
            }
        }
    }

    public void GoToTargetDestination (float target)
    {
            var x = pointA;
            var y = pointB;

            if (isTheBoss && currentLevel == 1)
            {
                pointA = LevelManager.Instance.lvl1PatrollA;
                pointB = LevelManager.Instance.lvl1PatrollB;
            }
            else if (isTheBoss && currentLevel == 2)
            {
                pointA = LevelManager.Instance.lvl2PatrollA;
                pointB = LevelManager.Instance.lvl2PatrollB;
            }

            if (currentPoint == LevelManager.Instance.bossPatrolllvl1AL || currentPoint == LevelManager.Instance.bossPatrolllvl2AL || currentPoint == LevelManager.Instance.bossPatrolllvl1BR || currentPoint == LevelManager.Instance.bossPatrolllvl2BR)
            {
                Flip();
                if (x == currentPoint)
                {
                    currentPoint = pointA.transform;
                }
                else if (y == currentPoint)
                {
                    currentPoint = pointB.transform;
                }
            }

        if (target == 1)
        {
            if (currentLevel == 2)
            {
                if (currentPoint == pointB.transform)
                {
                    Flip();
                    currentPoint = pointA.transform;
                }
                else
                {
                    if (isInRoom1)
                    {
                        destinationTarget = 0;
                        seekingDestination = false;
                    }
                }
            }
            else if (currentLevel == 1)
            {
                wantsToGoUp = true;
            }
            else if (currentLevel == 3)
            {
                wantsToGoDown = true;
            }
        }
        else if (target == 2)
        {
            if (currentLevel == 2)
            {
                if (currentPoint == pointA.transform)
                {
                    Flip();
                    currentPoint = pointB.transform;
                }
                else
                {
                    if (isInRoom1)
                    {
                        destinationTarget = 0;
                        seekingDestination = false;
                    }
                }
            }
            else if (currentLevel == 1)
            {
                wantsToGoUp = true;
            }
            else if (currentLevel == 3)
            {
                wantsToGoDown = true;
            }
        }
        else if (target == 3)
        {
            if (currentLevel == 1)
            {
                if (currentPoint == pointB.transform)
                {
                    Flip();
                    currentPoint = pointA.transform;
                }
                else
                {
                    if (isInRoom1)
                    {
                        destinationTarget = 0;
                        seekingDestination = false;
                    }
                }
            }
            else if (currentLevel == 2)
            {
                wantsToGoDown = true;
            }
            else if (currentLevel == 3)
            {
                wantsToGoDown = true;
            }
        }
        else if (target == 4)
        {
            if (currentLevel == 1)
            {
                if (currentPoint == pointA.transform)
                {
                    Flip();
                    currentPoint = pointB.transform;
                }
                else
                {
                    if (isInRoom1)
                    {
                        destinationTarget = 0;
                        seekingDestination = false;
                    }
                }
            }
            else if (currentLevel == 2)
            {
                wantsToGoDown = true;
            }
            else if (currentLevel == 3)
            {
                wantsToGoDown = true;
            }
        }
        else if (target == 5)
        {
            if (currentLevel == 3)
            {
                destinationTarget = 0;
                seekingDestination = false;
                //wantsToGoDown = true;
            }
            else if (currentLevel == 1)
            {
                wantsToGoUp = true;
            }
            else if (currentLevel == 2)
            {
                wantsToGoUp = true;
            }
        }
    }

    void MoveAround()
    {
        if (caughtPlayer)
        {
            PlayerController.Instance.transform.position = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
        }
        else if (comesFromTheBottom)
        {
            wantsToGoDown = true;
        }

        if (Vector3.Distance(transform.position, new Vector3(transform.position.x, yAxis, zAxis)) >= MinDist)
        {
            transform.position += (new Vector3(transform.position.x, yAxis, zAxis)- transform.position) * speed * Time.deltaTime;
        }

        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb2D.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb2D.velocity = new Vector2(-speed, 0);
        }

        if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
            if (isTheBoss)
            {
                var x = Random.Range(1, 5);
                if (x == 1)
                {
                    pointB = LevelManager.Instance.bossPatrolllvl1BL;
                    if (currentLevel == 2)
                    {
                        wantsToGoDown = true;
                    }
                }
                else if (x == 2)
                {
                    pointB = LevelManager.Instance.bossPatrolllvl1BR;
                    pointA = LevelManager.Instance.bossPatrolllvl1AR;
                    currentPoint = pointA.transform;
                    if (currentLevel == 2)
                    {
                        wantsToGoDown = true;
                    }
                }
                else if (x == 3)
                {
                    pointB = LevelManager.Instance.bossPatrolllvl2BL;
                    if (currentLevel == 2)
                    {
                        wantsToGoUp = true;
                    }
                }
                else if (x == 4)
                {
                    pointB = LevelManager.Instance.bossPatrolllvl2BR;
                    pointA = LevelManager.Instance.bossPatrolllvl2AR;
                    if (currentLevel == 2)
                    {
                        wantsToGoUp = true;
                    }
                }
            }
        }

        if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
            if (isTheBoss)
            {
                var x = Random.Range(1, 5);
                if (x == 1)
                {
                    pointA = LevelManager.Instance.bossPatrolllvl1AL;
                    pointB = LevelManager.Instance.bossPatrolllvl1BL;
                    currentPoint = pointB.transform;
                    if (currentLevel == 2)
                    {
                        wantsToGoDown = true;
                    }
                }
                else if (x == 2)
                {
                    pointA = LevelManager.Instance.bossPatrolllvl1AR;
                    if (currentLevel == 2)
                    {
                        wantsToGoDown = true;
                    }
                }
                else if (x == 3)
                {
                    pointA = LevelManager.Instance.bossPatrolllvl2AL;
                    pointB = LevelManager.Instance.bossPatrolllvl2BL;
                    currentPoint = pointB.transform;
                    if (currentLevel == 2)
                    {
                        wantsToGoUp = true;
                    }
                }
                else if (x == 4)
                {
                    pointA = LevelManager.Instance.bossPatrolllvl2AR;
                    if (currentLevel == 2)
                    {
                        wantsToGoUp = true;
                    }
                }
            }
        }
    }

    void ChasePLayer()
    {
        if (caughtPlayer)
        {
            PlayerController.Instance.canMove = false;
            PlayerController.Instance.rb2D.gravityScale = 0;
            PlayerController.Instance.colider.isTrigger = true;
            StopCoroutine("SurprisedToSeePlayer");
            StopCoroutine("CantSeePlayer");
            speed = startSpeed;
            searchingForPlayer = false;
            destinationTarget = 5;
            playerIsStealthed.boolSO = true;
            wantsToGoDown = false;

            if (currentPoint == pointA.transform && transform.position.x < 4.8f)
            {
                Flip();
                currentPoint = pointB.transform;
            }
            else if (currentPoint == pointB.transform && transform.position.x > 4.8f)
            {
                Flip();
                currentPoint = pointA.transform;
            }

            PlayerController.Instance.DropItem();
        }
        else
        {
            if (Vector3.Distance(transform.position, new Vector3(transform.position.x, transform.position.y, player.transform.position.z)) >= MinDist)
            {
                transform.position += (new Vector3(transform.position.x, transform.position.y, player.transform.position.z) - transform.position) * (speed/3) * Time.deltaTime;
            }

            if (Vector3.Distance(transform.position, player.transform.position) >= MinDist)
            {
                transform.position += (new Vector3(player.transform.position.x, player.transform.position.y,transform.position.z)- transform.position) * speed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && searchingForPlayer)
        {
            if (PlayerController.Instance.canMove == true || LevelManager.Instance.playerLevel != 3)
            {
                caughtPlayer = true;
            }
            else
            {
                caughtPlayer = false;
                destinationTarget = 0;
            }
        }
        
        if (collision.gameObject.tag == "Door")
        {
            var x = collision.GetComponent<door>();

            if (wantsToGoUp && x.upDoor)
            {
                currentLevel += 1;
                wantsToGoUp = false;
                transform.position = x.leadsToDoor.transform.position;

                if (!isTheBoss)
                {
                    if (currentPoint == pointA.transform)
                    {
                        pointB = LevelManager.Instance.lvl2PatrollB;
                        pointA = LevelManager.Instance.lvl2PatrollA;
                        currentPoint = pointA.transform;

                        zAxis = pointB.transform.position.z;
                        yAxis = pointB.transform.position.y;
                    }
                    else
                    {
                        pointB = LevelManager.Instance.lvl2PatrollB;
                        pointA = LevelManager.Instance.lvl2PatrollA;
                        currentPoint = pointB.transform;

                    }
                }
                else
                {
                    if (currentPoint == LevelManager.Instance.bossPatrolllvl2BL || currentPoint == LevelManager.Instance.bossPatrolllvl2AL)
                    {
                        if (facingRight)
                        {
                            Flip();
                            currentPoint = pointA.transform;
                        }
                    }
                    else if (currentPoint == LevelManager.Instance.bossPatrolllvl2BR || currentPoint == LevelManager.Instance.bossPatrolllvl2AR)
                    {
                        if (!facingRight)
                        {
                            Flip();
                            currentPoint = pointB.transform;
                        }
                    }
                }
                        zAxis = pointB.transform.position.z;
                        yAxis = pointB.transform.position.y;

                if (caughtPlayer)
                {
                    LevelManager.Instance.playerLevel += 1;
                }

                if (currentPoint == pointA.transform && transform.position.x < 5f)
                {
                    Flip();
                    currentPoint = pointB.transform;
                }
                else if (currentPoint == pointB.transform && transform.position.x > 5f)
                {
                    Flip();
                    currentPoint = pointA.transform;
                }
            }
            else if (wantsToGoDown && !x.upDoor)
            {
                currentLevel -= 1;
                wantsToGoDown = false;
                transform.position = x.leadsToDoor.transform.position;

                if (!isTheBoss)
                {
                    if (currentPoint == pointA.transform)
                    {
                        pointA = LevelManager.Instance.lvl1PatrollA;
                        currentPoint = pointA.transform;
                    }
                    else
                    {
                        pointB = LevelManager.Instance.lvl1PatrollB;
                        currentPoint = pointB.transform;
                    }
                }
                else
                {
                    if (currentPoint == LevelManager.Instance.bossPatrolllvl1BL || currentPoint == LevelManager.Instance.bossPatrolllvl1AL)
                    {
                        if (facingRight)
                        {
                            Flip();
                            currentPoint = pointA.transform;
                        }
                    }
                    else if (currentPoint == LevelManager.Instance.bossPatrolllvl1BR || currentPoint == LevelManager.Instance.bossPatrolllvl1AR)
                    {
                        if (!facingRight)
                        {
                            Flip();
                            currentPoint = pointB.transform;
                        }
                    }
                }

                zAxis = pointB.transform.position.z;
                    yAxis = pointB.transform.position.y;

                if (caughtPlayer)
                {
                    LevelManager.Instance.playerLevel -= 1;
                }
            }
        }

        if (collision.gameObject.tag == "Room")
        {
            GameObject x = null;

            if (RoomManager.Instance.correctRoom == 1)
            {
                x = RoomManager.Instance.room1;
            }
            else if (RoomManager.Instance.correctRoom == 2)
            {
                x = RoomManager.Instance.room2;
            }
            else if (RoomManager.Instance.correctRoom == 3)
            {
                x = RoomManager.Instance.room3;
            }
            else if (RoomManager.Instance.correctRoom == 4)
            {
                x = RoomManager.Instance.room4;
            }
            else if (RoomManager.Instance.correctRoom == 5)
            {
                x = RoomManager.Instance.room5;
            }

            if (collision.gameObject == RoomManager.Instance.room1)
            {
                isInRoom1 = true;
            }
            else if (collision.gameObject == RoomManager.Instance.room2)
            {
                isInRoom2 = true;
            }
            else if (collision.gameObject == RoomManager.Instance.room3)
            {
                isInRoom3 = true;
            }
            else if (collision.gameObject == RoomManager.Instance.room4)
            {
                isInRoom4 = true;
            }
            else if (collision.gameObject == RoomManager.Instance.room5)
            {
                isInRoom5 = true;
            }

            if (caughtPlayer && collision.gameObject == x)
            {
                PlayerController.Instance.gameObject.transform.parent = null;
                PlayerController.Instance.canMove = true;
                PlayerController.Instance.rb2D.gravityScale = 1;
                PlayerController.Instance.colider.isTrigger = false;
                PlayerController.Instance.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, PlayerController.Instance.zAxis);
                caughtPlayer = false;
                destinationTarget = 0;

                currentLevel = 2;
                playerIsStealthed.boolSO = false;

                if (isTheBoss)
                {
                    var y = Random.Range(1, 5);
                    if (y == 1)
                    {
                        pointB = LevelManager.Instance.bossPatrolllvl1BL;
                        if (currentLevel == 2)
                        {
                            wantsToGoDown = true;
                        }
                    }
                    else if (y == 2)
                    {
                        pointB = LevelManager.Instance.bossPatrolllvl1BR;
                        pointA = LevelManager.Instance.bossPatrolllvl1AR;
                        currentPoint = pointA.transform;
                        if (currentLevel == 2)
                        {
                            wantsToGoDown = true;
                        }
                    }
                    else if (y == 3)
                    {
                        pointB = LevelManager.Instance.bossPatrolllvl2BL;
                        if (currentLevel == 2)
                        {
                            wantsToGoUp = true;
                        }
                    }
                    else if (y == 4)
                    {
                        pointB = LevelManager.Instance.bossPatrolllvl2BR;
                        pointA = LevelManager.Instance.bossPatrolllvl2AR;
                        if (currentLevel == 2)
                        {
                            wantsToGoUp = true;
                        }
                    }
                }
                /*
                if (x == RoomManager.Instance.room1 || x == RoomManager.Instance.room3)
                {
                    Flip();
                    currentPoint = pointA.transform;
                }
                else
                {
                    Flip();
                    currentPoint = pointB.transform;
                }
                */
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            caughtPlayer = false;
        }

        if (collision.gameObject.tag == "Room")
        {
            if (collision.gameObject == RoomManager.Instance.room1)
            {
                isInRoom1 = false;
            }
            else if (collision.gameObject == RoomManager.Instance.room2)
            {
                isInRoom2 = false;
            }
            else if (collision.gameObject == RoomManager.Instance.room3)
            {
                isInRoom3 = false;
            }
            else if (collision.gameObject == RoomManager.Instance.room4)
            {
                isInRoom4 = false;
            }
            else if (collision.gameObject == RoomManager.Instance.room5)
            {
                isInRoom5 = false;
            }
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        facingRight = !facingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
