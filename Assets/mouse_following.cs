using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_following : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera computer_camera;
    [SerializeField] GameObject folder_icon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(folder_icon.activeSelf)
        {  Vector3 mousePos =computer_camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        transform.position = Vector2.MoveTowards(transform.position, mousePos2D, 0.5f);}
      

    }
}
