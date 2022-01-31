using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ManagerConsole : InteractiveObj
{
    public bool isElevatorActivated;

    private PlayerControl player;

    [SerializeField, BoxGroup("elevator")]
    private GameObject elevatorDoor;
    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    public override void interact()
    {
        isElevatorActivated = true;
        if (elevatorDoor.activeSelf)
        {
            player.NPCToTalk = gameObject;
            player.talkToNPC("MrRabbit.Manager_Office_Elevator_Access_Fixed");
        } else
        {
            player.NPCToTalk = gameObject;
            player.talkToNPC("MrRabbit.Manager_Office_Elevator_Access_UnFixed");
        }
        

        
    }
}
