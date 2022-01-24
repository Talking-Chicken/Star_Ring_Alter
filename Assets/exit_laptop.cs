using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit_laptop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject main_camera;
    public GameObject laptop_camera;
    Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = laptop_camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        if (Input.GetMouseButtonDown(0) && collider == Physics2D.OverlapPoint(mousePos2D))
        {


            Debug.Log("test");
            main_camera.SetActive(true);
            laptop_camera.SetActive(false);

        }
    }
}
