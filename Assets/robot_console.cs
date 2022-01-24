using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_console : InteractiveObj
{
    // Start is called before the first frame update
    public GameObject robot;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void interact()
    {
        robot.SetActive(true);
     
    }
}