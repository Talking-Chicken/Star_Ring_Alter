using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_schedule_system : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    public float step;
    public Transform target;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    // Update is called once per frame
    
        

        void FixedUpdate()
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, target.position, step);

            Vector2 currentPos = rbody.position;
          
            
            Vector2 inputVector = Vector2.ClampMagnitude(newPos-currentPos, 1);
          

            isoRenderer.SetDirection(inputVector);
            rbody.MovePosition(newPos);
        }
    }
