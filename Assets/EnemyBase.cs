using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject pointA, pointB;
    public GameObject sightRange, alertSightRange, eyes;
    Rigidbody2D rb2D;
    Animator anim;
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
    public bool isInRoom1, isInRoom2, isInRoom3, isInRoom4;
    public float destinationTarget = 0;
    bool seekingDestination;
    bool wantsToGoUp, wantsToGoDown;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        //anim.SetBool("isRunning", true);
        sprite = GetComponent<SpriteRenderer>();

        startColor = sprite.color;
        startSpeed = speed;

        player = PlayerController.Instance.gameObject;
        zAxis = transform.position.z;
        yAxis = transform.position.y;
    }

    private void Update()
    {
        GoToTargetDestination(destinationTarget);

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
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = startColor;
            speed = startSpeed;
        }
    }

    private void FixedUpdate()
    {
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
        }
    }

    void MoveAround()
    {
        if (caughtPlayer)
        {
            PlayerController.Instance.transform.position = transform.position;
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

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
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
            destinationTarget = 1;
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
        if (collision.gameObject.tag == "Player")
        {
            caughtPlayer = true;
        }
        
        if (collision.gameObject.tag == "Door")
        {
            Debug.Log("f");
            var x = collision.GetComponent<door>();

            if (wantsToGoUp && x.upDoor)
            {
                currentLevel += 1;
                wantsToGoUp = false;
                transform.position = x.leadsToDoor.transform.position;

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

                    zAxis = pointB.transform.position.z;
                    yAxis = pointB.transform.position.y;
                }

                if (caughtPlayer)
                {
                    LevelManager.Instance.playerLevel += 1;
                }
            }
            else if (wantsToGoDown && !x.upDoor)
            {
                currentLevel -= 1;
                wantsToGoDown = false;
                transform.position = x.leadsToDoor.transform.position;

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

            if (caughtPlayer && collision.gameObject == x)
            {
                PlayerController.Instance.gameObject.transform.parent = null;
                PlayerController.Instance.canMove = true;
                PlayerController.Instance.rb2D.gravityScale = 1;
                PlayerController.Instance.colider.isTrigger = false;
                PlayerController.Instance.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, PlayerController.Instance.zAxis);
                caughtPlayer = false;
                destinationTarget = 0;

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
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            caughtPlayer = false;
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
