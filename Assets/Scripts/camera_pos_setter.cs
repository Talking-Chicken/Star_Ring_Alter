using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_pos_setter : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 player_area;
    public static bool rabbit_on;
    void Start()
    {
        player_area = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player_area;
    }
}
