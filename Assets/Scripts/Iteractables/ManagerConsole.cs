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

    private bool triggered = false;
    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    public override void interact()
    {
        isElevatorActivated = true;
        if (!triggered) {
            triggered = true;
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
        StartCoroutine(waitToChangeToExploreState());
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.GetComponent<PlayerControl>() != null) {
            StartCoroutine(waitToCallChangeState());
            Debug.Log("eb");
        }
    }

    IEnumerator waitToCallChangeState() {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(changeToExplore());
    }

    IEnumerator changeToExplore() {
        yield return new WaitForSeconds(2.0f);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
    }
}
