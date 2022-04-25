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
        // state.transitionState(State.UI);
        //UIContainer.SetActive(true);
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
    
        if (playerNeuroDevice.search(playerNeuroDevice.downloadedApps, "engineering module"))
        {
            if (getGearCount() >= 3)
                player.talkToSelf("Response_player_action.interact_door.3");
            else if (getGearCount() >= 1)
                player.talkToSelf("Response_player_action.interact_door.2");
            else
                player.talkToSelf("Response_player_action.interact_door.1");
        } else
        {
            player.talkToSelf("Response_player_action.check_broken_door");
        }

        
    }

    public void confirm()
    {
        if (playerNeuroDevice.search(playerNeuroDevice.downloadedApps, "engineering module"))
        {
            if (getGearCount() >= 3)
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

    public override void useItem()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf("Response_player_action.use_item_on_door");
    }

    public override void useNeuroImplant()
    {
        NeuroImplantApp app = NeuroGUIControl.currentUnit.NeuroApp;
        if (app.GetComponent<EngineeringModule>() != null) {

            if (getGearCount() >= 3) {
                door.door.SetActive(true);
                door.gameObject.SetActive(false);

                if (talkingArea != null)
                    talkingArea.SetActive(true);

                for (int i = playerBackpack.backpack.Count-1; i >= 0; i--) {
                    if (playerBackpack.backpack[i].GetComponent<Item>().ItemName.ToLower().Trim().Contains("mechanical parts"))
                        playerBackpack.backpack.RemoveAt(i);
                }
               /* PlayerControl player = FindObjectOfType<PlayerControl>();
                player.ChangeState(player.stateExplore);
                player.talkToSelf("Response_player_action.interact_door.5");*/

            } else {
                PlayerControl player = FindObjectOfType<PlayerControl>();
                player.ChangeState(player.stateExplore);
                player.talkToSelf("Response_player_action.interact_door.2");
            }
        } else {
            PlayerControl player = FindObjectOfType<PlayerControl>();
            player.ChangeState(player.stateExplore);
            player.talkToSelf("Response_player_action.interact_door.4");
        }  
    }

    public int getGearCount() {
        int gearCount = 0;
            for (int i = 0; i < playerBackpack.backpack.Count; i++) {
                if (playerBackpack.backpack[i].GetComponent<Item>().ItemName.ToLower().Trim().Contains("mechanical parts"))
                    gearCount++;
            }
        return gearCount;
    }
}
