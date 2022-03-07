using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BrokenDoor : InteractiveObj
{
    [SerializeField] private broken_door door;

    //the area that trigger a dialogue between player and Mr.Rabbit, when fixed the broken door
    [SerializeField] private GameObject talkingArea;

    [SerializeField] GameObject UIContainer;
    [SerializeField] TextMeshProUGUI description;

    private NeuroImplantDevice playerNeuroDevice;
    private PlayerBackpack playerBackpack;
    private StateManager state;

    private void Start()
    {
        playerNeuroDevice = FindObjectOfType<PlayerControl>().GetComponent<NeuroImplantDevice>();
        playerBackpack = FindObjectOfType<PlayerBackpack>();
        state = FindObjectOfType<StateManager>();
    }

    public override void interact()
    {
        state.transitionState(State.UI);
        UIContainer.SetActive(true);
        if (playerNeuroDevice.search(playerNeuroDevice.downloadedApps, "engineering module"))
        {
            if (playerBackpack.contains("gear 5") && playerBackpack.contains("gear 4") && playerBackpack.contains("gear 3"))
                description.text = "spend three gears to fix the door";
            else if (playerBackpack.contains("gear 5") || playerBackpack.contains("gear 4") || playerBackpack.contains("gear 3"))
                description.text = "find more gears to fix the door";
            else
                description.text = "I know how to fix it, but I need gears to fix it";
        } else
        {
            description.text = "it's a broken door, I have no idea how to fix it";
        }

        
    }

    public void confirm()
    {
        if (playerNeuroDevice.search(playerNeuroDevice.downloadedApps, "engineering module"))
        {
            if (playerBackpack.contains("gear 5") && playerBackpack.contains("gear 4") && playerBackpack.contains("gear 3"))
            {
                door.door.SetActive(true);
                door.gameObject.SetActive(false);

                if (talkingArea != null)
                    talkingArea.SetActive(true);
            }
        }
        UIContainer.SetActive(false);
        state.transitionState(State.Explore);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
    }

    public void exit()
    {
        UIContainer.SetActive(false);
        state.transitionState(State.Explore);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
    }
}
