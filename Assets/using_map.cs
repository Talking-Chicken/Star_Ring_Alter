using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class using_map : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool map_on;
    public GameObject map;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControl.CanMove && Input.GetKey(KeyCode.M)) {
            map.SetActive(true);
            PlayerControl.CanMove = false;
        }

        if (map.activeSelf && Input.GetKey(KeyCode.M)) {
            map.SetActive(false);
            PlayerControl.CanMove = true;
        }
    }
}
