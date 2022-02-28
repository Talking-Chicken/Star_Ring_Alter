using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamic_camera : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera main_camera;
    public float static_fov;
    public float moving_fov;
    private float t;
    private float i;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (IsometricPlayerMovementController.movement==new Vector2(0,0)) {

            i = 0;
            main_camera.orthographicSize = Mathf.Lerp(moving_fov, static_fov, t);
            t += 2f * Time.deltaTime;

        }
        else
        {
            t = 0;
            main_camera.orthographicSize = Mathf.Lerp(static_fov, moving_fov, i);
           i += 2f * Time.deltaTime;
        }
     



    }
}
