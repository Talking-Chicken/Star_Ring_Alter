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
        if (playerBackpack.contains("access card") && !laptop.activeSelf)
        {
            state.transitionState(State.UI);
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
        base.useItem();
        Item currentItem = InventoryGUIControl.currentUnit.items.Peek();
        if (currentItem.ItemName.ToLower().Trim().Contains("access card")) {
            laptop.SetActive(true);
            Cursor.visible = false;
            laptopPOV.SetActive(true);

            //talk about the laptop with Mr.Rabbit
            talk.getPlayer().NPCToTalk = gameObject;
            talk.getPlayer().talkToNPC();
        }
    }

    IEnumerator waitToChangeState(PlayerStateBase newState) {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<PlayerControl>().ChangeState(newState);
    }
}
