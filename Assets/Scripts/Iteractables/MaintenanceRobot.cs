using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class MaintenanceRobot : InteractiveObj
{
    [SerializeField] GameObject UIContainer;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField, Tooltip("this is the icon for maintenance robot, when it turns into an item")] 
    Sprite itemIcon;

    private PlayerBackpack playerBackpack;
    private StateManager state;
    private Talkable talk;

    [SerializeField, BoxGroup("talking area")]
    private GameObject talkingArea;
    void Start()
    {
        playerBackpack = FindObjectOfType<PlayerBackpack>();
        state = FindObjectOfType<StateManager>();
        talk = GetComponent<Talkable>();
    }

    public override void interact()
    {
        state.transitionState(State.UI);
        UIContainer.SetActive(true);
        if (playerBackpack.contains("operating software"))
            description.text = "use operating software to active the robot?";
        else
            description.text = "this dude is broken";

        //talk about the laptop with Mr.Rabbit
        talk.getPlayer().NPCToTalk = gameObject;
        talk.getPlayer().talkToNPC();
    }

    public void exitUI()
    {
        UIContainer.SetActive(false);
        state.transitionState(State.Explore);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
    }

    public void confirm()
    {
        if (playerBackpack.contains("operating software"))
        {
            gameObject.transform.parent.gameObject.AddComponent<MaintenanceRobotItem>();
            GetComponentInParent<MaintenanceRobotItem>().setIcon(itemIcon);
            playerBackpack.remove("operating software");
            UIContainer.SetActive(false);
            FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);

            talkingArea.SetActive(true);

            Destroy(this);
        }else
        {
            UIContainer.SetActive(false);
        }
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);

    }

    public override void useItem()
    {
        Item currentItem = InventoryGUIControl.currentUnit.items.Peek();
        if (currentItem.ItemName.ToLower().Trim().Contains("operating software")) {
            gameObject.transform.parent.gameObject.AddComponent<MaintenanceRobotItem>();
            GetComponentInParent<MaintenanceRobotItem>().setIcon(itemIcon);
            playerBackpack.remove("operating software");
            UIContainer.SetActive(false);
            FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);

            talkingArea.SetActive(true);

            Destroy(this);
        }
    }
}
