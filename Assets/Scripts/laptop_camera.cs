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
        if (playerBackpack.contains("access card") && !laptop.activeSelf)
        {
            player.ChangeState(player.stateUI);
            laptop.SetActive(true);
            Cursor.visible = false;
            laptopPOV.SetActive(true);

            //talk about the laptop with Mr.Rabbit
            talk.getPlayer().NPCToTalk = gameObject;
            talk.getPlayer().talkToNPC();

            //Display.displays[1].Activate();
        } else {
            StartCoroutine(waitToChangeState(FindObjectOfType<PlayerControl>().stateExplore));
        }
    }

    public override void useItem()
    {
        Item currentItem = InventoryGUIControl.currentUnit.items.Peek();
        PlayerControl player = FindObjectOfType<PlayerControl>();
        if (currentItem.ItemName.ToLower().Trim().Contains("access card")) {
            player.ChangeState(player.stateUI);
            laptop.SetActive(true);
            Cursor.visible = false;
            laptopPOV.SetActive(true);

            //talk about the laptop with Mr.Rabbit
            talk.getPlayer().NPCToTalk = gameObject;
            talk.getPlayer().talkToNPC();
        } else {
            //TODO: describe what happens if player trying to use other items
        }
    }

    public override void useNeuroImplant()
    {
        //TODO: describe what happens if player trying to use neuro implant for rita's laptop
    }

    IEnumerator waitToChangeState(PlayerStateBase newState) {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<PlayerControl>().ChangeState(newState);
    }
}
