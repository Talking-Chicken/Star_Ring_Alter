using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;
    public Vector2 dir { get; private set; } //direction that player is facing [cannot be (0,0)]
    private Vector2 movement = new Vector2(0, 0); //direction that player is going [can be (0,0)]

    private PlayerControl player; //custom script

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();

        player = GetComponent<PlayerControl>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.canMove)
        {
            setDir();
            Vector2 currentPos = rbody.position;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * movementSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            isoRenderer.SetDirection(movement);
            rbody.MovePosition(newPos);
        }
        
    }
    void setDir() //give dir a Vector2 value, according to WASD that player is pressing
    {
        if (Input.GetAxis("Vertical")>0)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                dir = new Vector2(-0.75f, 0.75f);
                movement = dir;
            }
            else if (Input.GetAxis("Horizontal") >0)
            {
                dir = new Vector2(0.75f, 0.75f);
                movement = dir;
            }
            else
            {
                dir = new Vector2(0, 1);
                movement = dir;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            if (Input.GetAxis("Horizontal")<0)
            {
                dir = new Vector2(-0.75f, -0.75f);
                movement = dir;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                dir = new Vector2(0.75f, -0.75f);
                movement = dir;
            }
            else
            {
                dir = new Vector2(0, -1);
                movement = dir;
            }
        }
        else
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                dir = new Vector2(-1, 0);
                movement = dir;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                dir = new Vector2(1, 0);
                movement = dir;
            }
            else
            {
                movement = new Vector2(0, 0);
            }
        }
    }
}
