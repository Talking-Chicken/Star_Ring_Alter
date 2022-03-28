using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class using_map : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool map_on;
    public GameObject map;
    public GameObject main_camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M)&&PlayerControl.show_invest) {
            if (map.activeSelf)
            {
                map.SetActive(false);

                main_camera.SetActive(true);
            } else
            {
                map.SetActive(true);

                main_camera.SetActive(false);
            }
           
        }

        
    }
}
