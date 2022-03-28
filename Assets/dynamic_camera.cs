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
    public float current_fov1;

    public float delay = 0.5f;
   
    float elapsed;
    float elapsed1;
    void Start()
    {
        current_fov1 = main_camera.orthographicSize;
        current_fov = main_camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
       //Debug.Log(IsometricPlayerMovementController.movement);
       
        if (IsometricPlayerMovementController.movement==new Vector2(0,0)) {
            elapsed1 += Time.deltaTime;
            if (elapsed1 > delay)
            {
                elapsed = 0f;
                i = 0;
                main_camera.orthographicSize = Mathf.Lerp(current_fov, static_fov, t);
                t += 1f * Time.deltaTime;
                current_fov1 = main_camera.orthographicSize;
            }
}
        else
        {
            elapsed += Time.deltaTime;
            if (elapsed>delay)
            {
                elapsed1 = 0f;
                t = 0;
                main_camera.orthographicSize = Mathf.Lerp(current_fov1, moving_fov, i);
                i += 1f * Time.deltaTime;
                current_fov = main_camera.orthographicSize;
            }
        }
     



    }
}
