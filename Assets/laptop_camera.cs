using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laptop_camera : InteractiveObj
{
    PlayerBackpack playerBackpack;

    private void Start()
    {
        playerBackpack = FindObjectOfType<PlayerBackpack>();
    }

    public override void interact()
    {
        if (playerBackpack.contains("access card"))
        {
            Debug.Log("test");
            Cursor.visible = false;
            //Display.displays[1].Activate();
        }
    }
}
