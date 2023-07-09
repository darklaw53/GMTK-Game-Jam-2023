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
    public bool inFrontOfWindow;
    public ItemYouCanTake inFrontOfItem;

    public float zAxis, yAxis;
    door currentDoor;

    public ItemYouCanTake heldItem;
    public bool sendingItem;

    public Animator anim;

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
        if (moveHorizontal != 0 && canMove)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        if (currentDoor != null && canMove && Input.GetKeyDown(KeyCode.Space))
        {
            if (currentDoor.upDoor)
            {
                LevelManager.Instance.playerLevel += 1;
            }
            else
            {
                LevelManager.Instance.playerLevel -= 1;
            }

            transform.position = new Vector3(transform.position.x, currentDoor.leadsToDoor.transform.position.y, transform.position.z);
        }
        else if (inFrontOfWindow && canMove && Input.GetKeyDown(KeyCode.Space) && !sendingItem)
        {
            sendingItem = true;
            WindowManager.Instance.TakeObject(heldItem.itemName, heldItem.transform.gameObject);
            heldItem = null;
        }
        else if (inFrontOfItem != null && canMove && Input.GetKeyDown(KeyCode.Space) && heldItem == null)
        {
            heldItem = inFrontOfItem;
            heldItem.transform.parent = transform;
        }
        else if (inFrontOfItem == null && canMove && Input.GetKeyDown(KeyCode.Space) && heldItem != null)
        {
            heldItem.transform.parent = null;
            heldItem = null;
        }

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

    public void DropItem()
    {
        if (heldItem != null)
        {
            heldItem = null;
            heldItem.transform.parent = null;
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

        if (collision.gameObject.tag == "Door")
        {
            currentDoor = collision.GetComponent<door>();
        }

        if (collision.gameObject.tag == "Window")
        {
            inFrontOfWindow = true;
        }

        if (collision.gameObject.tag == "Item")
        {
            inFrontOfItem = collision.GetComponent<ItemYouCanTake>();
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

        if (collision.gameObject.tag == "Door")
        {
            currentDoor = null;
        }

        if (collision.gameObject.tag == "Window")
        {
            inFrontOfWindow = false;
        }

        if (collision.gameObject.tag == "Item")
        {
            inFrontOfItem = null;
        }
    }
}
