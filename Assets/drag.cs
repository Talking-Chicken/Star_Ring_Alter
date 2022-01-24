using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    bool canMove;
    bool dragging;
    Collider2D collider;
  
    public Camera computer_camera;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        canMove = false;
        dragging = false;
       

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = computer_camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        //Debug.Log(Physics2D.OverlapPoint(mousePos2D));
      
        if (Input.GetMouseButtonDown(0) && GameObject.Find("rita_laptop_0").GetComponent<laptop_manager>().loading==false)
        {
            
            if (collider == Physics2D.OverlapPoint(mousePos2D))
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
            if (canMove)
            {
                dragging = true;
            }


        }
        if (dragging)
        {
            this.transform.position = mousePos2D;
            GameObject.Find("rita_laptop_0").GetComponent<laptop_manager>().drag_object = gameObject;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
        }
     
    }

   
           
    

}
