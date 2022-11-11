using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue_direction_adjuster : MonoBehaviour
{
    // Start is called before the first frame update

    Vector2 differencePos;
    PlayerControl player;
    public static Vector2 movement = new Vector2(0, 0);
    public Vector2 dir { get; private set; }
    void Start()
    {
         player = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void set_direction()
    {/*
        Debug.Log("player control"+player_control);
        Debug.Log("interacting obj" + player_control.InteractingObj);
        Debug.Log(" parent" + player_control.InteractingObj.transform.parent);*/
        if (player.InteractingObj.transform.parent.GetComponent<Isometric_AI_Render>() != null)
        {
            differencePos = player.InteractingObj.transform.parent.transform.position - player.transform.position;
            setDir();
            player.transform.GetComponent<IsometricCharacterRenderer>().resetdir(movement);

            differencePos = player.transform.position-player.InteractingObj.transform.parent.transform.position;
            setDir();
            player.InteractingObj.transform.parent.GetComponent<Isometric_AI_Render>().resetdir(movement);
        }

    }
    void setDir() //give dir a Vector2 value, according to WASD that player is pressing
    {
        //  Debug.Log(movement);
        if (differencePos.y > 0)
        {
            if (differencePos.x < 0)
            {
                dir = new Vector2(-0.75f, 0.75f);
                movement = dir;
            }
            else if (differencePos.x > 0)
            {
                dir = new Vector2(0.75f, 0.75f);
                movement = dir;
            }
            else
            {
                dir = new Vector2(0, 1);
                movement = dir;
            }
        }
        else if (differencePos.y < 0)
        {
            if (differencePos.x < 0)
            {
                dir = new Vector2(-0.75f, -0.75f);
                movement = dir;
            }
            else if (differencePos.x > 0)
            {
                dir = new Vector2(0.75f, -0.75f);
                movement = dir;
            }
            else
            {
                dir = new Vector2(0, -1);
                movement = dir;
            }
        }
        else
        {
            if (differencePos.x < 0)
            {
                dir = new Vector2(-1, 0);
                movement = dir;
            }
            else if (differencePos.x > 0)
            {
                dir = new Vector2(1, 0);
                movement = dir;
            }
            else
            {
                movement = new Vector2(0, 0);
            }
        }
    }
}
