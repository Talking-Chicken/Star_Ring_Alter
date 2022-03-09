using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamic_camera : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera main_camera;
    public float static_fov;
    public float moving_fov;
    public float current_fov;
    private float t;
    private float i;

    float delay = 1.5f;
   
    float elapsed;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (IsometricPlayerMovementController.movement==new Vector2(0,0)) {
            elapsed = 0f;
            i = 0;
            main_camera.orthographicSize = Mathf.Lerp(current_fov, static_fov, t);
            t += 1f * Time.deltaTime;

        }
        else
        {
            elapsed += Time.deltaTime;
            if (elapsed>delay)
            {
                t = 0;
                main_camera.orthographicSize = Mathf.Lerp(static_fov, moving_fov, i);
                i += 1f * Time.deltaTime;
                current_fov = main_camera.orthographicSize;
            }
        }
     



    }
}
