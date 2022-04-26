using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class laptop_camera : InteractiveObj
{
    PlayerBackpack playerBackpack;
    StateManager state;
    [SerializeField] GameObject laptop;
    [SerializeField] GameObject laptopPOV;
    public static bool workdone;

    private Talkable talk;
    private void Start()
    {
        state = FindObjectOfType<StateManager>();
        playerBackpack = FindObjectOfType<PlayerBackpack>();
        talk = GetComponent<Talkable>();
    }

    public override void interact()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf("Response_player_action.Rita.1");
    }

    public override void useItem()
    {
        Item currentItem = InventoryGUIControl.currentUnit.items.Peek();
        PlayerControl player = FindObjectOfType<PlayerControl>();
     
          
      
          
      
            if (currentItem.ItemName.ToLower().Trim().Contains("access card"))
            {
                Item accessCard = playerBackpack.getItem("access card");

                if (accessCard.GetComponent<AccessCard>().level < 2)
                {
                    player.ChangeState(player.stateUI);
                    laptop.SetActive(true);
                    Cursor.visible = false;
                    laptopPOV.SetActive(true);

                    //talk about the laptop with Mr.Rabbit
                    talk.getPlayer().NPCToTalk = gameObject;
                    talk.getPlayer().talkToNPC();
                }
                else
                {
                    player.ChangeState(player.stateExplore);
                    Player.talkToNPC("Response.laptop");
                }

            }
            else
            {

                player.ChangeState(player.stateExplore);
                player.talkToSelf("Response_player_action.Rita.1");
            }
        
        
    }

    public override void useNeuroImplant()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf("Response_player_action.Rita.2");
    }

    IEnumerator waitToChangeState(PlayerStateBase newState) {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<PlayerControl>().ChangeState(newState);
    }
}
