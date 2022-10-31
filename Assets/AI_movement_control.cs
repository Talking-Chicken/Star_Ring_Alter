using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_movement_control : MonoBehaviour
{
    public float movementSpeed = 1f;
    Isometric_AI_Render isoRenderer;

    Rigidbody2D rbody;
    public Vector2 dir { get; private set; } //direction that player is facing [cannot be (0,0)]
    public static Vector2 movement = new Vector2(0, 0); //direction that player is going [can be (0,0)]

    private PlayerControl player; //custom script
    Vector2 currentPos;
   public  Vector2 targetPos;
    Vector2 differencePos;

    private void Awake()
    {
      
        isoRenderer = GetComponentInChildren<Isometric_AI_Render>();

        player = GetComponent<PlayerControl>();
        rbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        setDir();
        currentPos = rbody.position;
        
     
          //  Vector2 movement = inputVector * movementSpeed;
       //     Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            isoRenderer.SetDirection(movement);
        differencePos = targetPos - currentPos;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime*movementSpeed;
        rbody.MovePosition(newPos);

    }
    void setDir() //give dir a Vector2 value, according to WASD that player is pressing
    {
        if (differencePos.y > 0)
        {
            if (differencePos.x < 0)
            {
                dir = new Vector2(-0.75f, 0.75f);
                movement = dir;
            }
            else if (differencePos.x > 0)
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
        else if (differencePos.y < 0)
        {
            if (differencePos.x < 0)
            {
                dir = new Vector2(-0.75f, -0.75f);
                movement = dir;
            }
            else if (differencePos.x > 0)
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
            if (differencePos.x < 0)
            {
                dir = new Vector2(-1, 0);
                movement = dir;
            }
            else if (differencePos.x > 0)
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
