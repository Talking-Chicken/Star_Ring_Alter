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

    public override void useNeuroImplant()
    {
        NeuroImplantApp app = NeuroGUIControl.currentUnit.NeuroApp;
        if (app.GetComponent<EngineeringModule>() != null) {
            int gearCount = 0;
            for (int i = 0; i < playerBackpack.backpack.Count; i++) {
                if (playerBackpack.backpack[i].GetComponent<Item>().ItemName.ToLower().Trim().Contains("gear"))
                    gearCount++;
            }

            if (gearCount >= 3) {
                door.door.SetActive(true);
                door.gameObject.SetActive(false);

                if (talkingArea != null)
                    talkingArea.SetActive(true);

                for (int i = playerBackpack.backpack.Count-1; i >= 0; i--) {
                    if (playerBackpack.backpack[i].GetComponent<Item>().ItemName.ToLower().Trim().Contains("gear"))
                        playerBackpack.backpack.RemoveAt(i);
                }
            } else {
                //TODO: mention about that you don't have enough gear
            }
        } else {
            //TODO: say something if player not showing engineering module
        }
           
    }
}
