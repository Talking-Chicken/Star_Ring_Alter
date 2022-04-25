using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_console : InteractiveObj
{
    public GameObject robot;
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource audio;
    public override void interact()
    {
        audio.PlayOneShot(clip);
        if (FindObjectOfType<DeliveryTunnel>().sentRobot) { robot.SetActive(true); StartCoroutine(waitToChangeToExploreState()); } else
        {
            PlayerControl player = FindObjectOfType<PlayerControl>();
            player.ChangeState(player.stateExplore);
            player.talkToSelf("Response.robot_control");
        }
          
        
        
    }
}