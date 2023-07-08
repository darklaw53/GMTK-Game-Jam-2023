using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject pointA, pointB;
    public GameObject sightRange, alertSightRange;
    Rigidbody2D rb2D;
    Animator anim;
    Transform currentPoint;

    public float speed;
    float startSpeed;

    SpriteRenderer sprite;
    Color startColor;

    public BoolSO playerIsStealthed;
    public float patience;
    bool searchingForPlayer, caughtPlayer;

    GameObject player;
    public float MinDist, MaxDist;
    float zAxis, yAxis;
    bool seesPlayer;

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
        var hit = Physics2D.Linecast(transform.position, sightRange.transform.position, 1 << LayerMask.NameToLayer("Player"));
        if (searchingForPlayer)
        {
            hit = Physics2D.Linecast(transform.position, alertSightRange.transform.position, 1 << LayerMask.NameToLayer("Player"));
        }

        if (hit.collider != null && !playerIsStealthed.boolSO)
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

    void MoveAround()
    {
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

        }

        if (Vector3.Distance(transform.position, new Vector3(transform.position.x, transform.position.y, player.transform.position.z)) >= MinDist)
        {
            transform.position += (new Vector3(transform.position.x, transform.position.y, player.transform.position.z) - transform.position) * (speed/3) * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, player.transform.position) >= MinDist)
        {
            transform.position += (new Vector3(player.transform.position.x, player.transform.position.y,transform.position.z)- transform.position) * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            caughtPlayer = true;
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
