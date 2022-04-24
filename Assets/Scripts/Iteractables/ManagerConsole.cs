using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ManagerConsole : InteractiveObj
{
    public bool isElevatorActivated;


    [SerializeField, BoxGroup("elevator")]
    private GameObject elevatorDoor;

    private bool triggered = false;

    public override void interact()
    {
        isElevatorActivated = true;
        if (!triggered) {
            triggered = true;
            Player.ChangeState(Player.stateExplore);
            if (elevatorDoor.activeSelf)
            {
                Player.NPCToTalk = gameObject;
                Player.talkToNPC("MrRabbit.Manager_Office_Elevator_Access_Fixed");
            } else
            {
                Player.NPCToTalk = gameObject;
                Player.talkToNPC("MrRabbit.Manager_Office_Elevator_Access_UnFixed");
            }
        }
    }
}
