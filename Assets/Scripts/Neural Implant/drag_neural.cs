using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag_neural : MonoBehaviour
{
    bool canMove;
    bool dragging;
    Collider2D collider;
    Vector2 orginal_pos;
    public Camera neural_computer;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        canMove = false;
        dragging = false;
        orginal_pos = transform.position;

    } // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = neural_computer.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        //Debug.Log(Physics2D.OverlapPoint(mousePos2D));

        if (Input.GetMouseButtonDown(0))
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
                //blocker_manager.once = true;
            }


        }
        if (dragging)
        {
            this.transform.position = mousePos2D;
            blocker_manager.drag_object = this.gameObject;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
            if (blocker_manager.drag_object ==gameObject) {  blocker_manager.drag_object = null; }
            
        }
    }
    public  void  back()
    { this.transform.position = orginal_pos; }
}
