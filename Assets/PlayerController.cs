using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public Rigidbody2D rb2D;
    public BoxCollider2D colider;

    public float moveSpeed, jumpForce;
    float moveHorizontal, moveVertical;
    bool isFacingRight;

    public bool canMove;

    public float zAxis, yAxis;

    private void Start()
    {
        canMove = true;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        colider = gameObject.GetComponent<BoxCollider2D>();

        zAxis = transform.position.z;
        yAxis = transform.position.y;
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        Flip();
    }

    private void FixedUpdate()
    {
        if (canMove) rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, rb2D.velocity.y);
    }

    void Flip()
    {
        if (isFacingRight && moveHorizontal < 0f || !isFacingRight && moveHorizontal > 0f && canMove)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == RoomManager.Instance.room1 & RoomManager.Instance.correctRoom == 1)
        {
            RoomManager.Instance.isInCorrectRoom = true;
        }
        else if (collision.gameObject == RoomManager.Instance.room2 & RoomManager.Instance.correctRoom == 2)
        {
            RoomManager.Instance.isInCorrectRoom = true;
        }
        else if (collision.gameObject == RoomManager.Instance.room3 & RoomManager.Instance.correctRoom == 3)
        {
            RoomManager.Instance.isInCorrectRoom = true;
        }
        else if (collision.gameObject == RoomManager.Instance.room4 & RoomManager.Instance.correctRoom == 4)
        {
            RoomManager.Instance.isInCorrectRoom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == RoomManager.Instance.room1 & RoomManager.Instance.correctRoom == 1)
        {
            RoomManager.Instance.isInCorrectRoom = false;
        }
        else if (collision.gameObject == RoomManager.Instance.room2 & RoomManager.Instance.correctRoom == 2)
        {
            RoomManager.Instance.isInCorrectRoom = false;
        }
        else if (collision.gameObject == RoomManager.Instance.room3 & RoomManager.Instance.correctRoom == 3)
        {
            RoomManager.Instance.isInCorrectRoom = false;
        }
        else if (collision.gameObject == RoomManager.Instance.room4 & RoomManager.Instance.correctRoom == 4)
        {
            RoomManager.Instance.isInCorrectRoom = false;
        }
    }
}
