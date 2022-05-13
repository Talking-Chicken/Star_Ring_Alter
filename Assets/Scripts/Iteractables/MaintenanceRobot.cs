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

    private PlayerBackpack playerbackpack;
    private StateManager state;
    private Talkable talk;
    private bool first_time;

    [SerializeField, BoxGroup("talking area")]
    private GameObject talkingArea;
    [SerializeField] string[] parts;
    void Start()
    {
        playerbackpack = FindObjectOfType<PlayerBackpack>();
        state = FindObjectOfType<StateManager>();
        talk = GetComponent<Talkable>();
    }

    public override void interact()
    {
        // state.transitionState(State.UI);
        //UIContainer.SetActive(true);
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);

        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("check_broken_robot") && parts[2].Equals("FALSE"))
            {


                random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "TRUE" + "," + parts[3];
                first_time = true;
                break;
            } else if (parts[0].Equals("check_broken_robot") && parts[2].Equals("TRUE")) { first_time = false;break; }
           
        }

        if (first_time) { player.talkToSelf("MrRabbit.Maintenance_Robot_Start"); } 
        
        else if (playerbackpack.contains("Maintenance Protocol"))
            player.talkToSelf("Response_player_action.interact_robot.2");
        else
            player.talkToSelf("Response_player_action.interact_robot.1");
    }

    public override void useItem()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        Item currentItem = InventoryGUIControl.currentUnit.items.Peek();
        if (currentItem.ItemName.ToLower().Trim().Contains("Maintenance Protocol".ToLower().Trim())) {
            gameObject.transform.parent.gameObject.AddComponent<MaintenanceRobotItem>();
            GetComponentInParent<MaintenanceRobotItem>().setIcon(itemIcon);
            playerbackpack.remove("Maintenance Protocol");
            UIContainer.SetActive(false);
            player.ChangeState(player.stateExplore);

            //talkingArea.SetActive(true);
            player.talkToSelf("MrRabbit.Maintenance_Robot_Fixed");
            Destroy(this);
        } else {
            player.ChangeState(player.stateExplore);
            player.talkToSelf("Response_player_action.interact_robot.3");
        }
        base.useItem();
    }

    public override void useNeuroImplant()
    {
       
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf("Response_player_action.interact_door.6");
        base.useNeuroImplant();
    }

    public void exitUI()
    {
        UIContainer.SetActive(false);
        state.transitionState(State.Explore);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
    }

    public void confirm()
    {
        if (playerbackpack.contains("Maintenance Protocol"))
        {
            gameObject.transform.parent.gameObject.AddComponent<MaintenanceRobotItem>();
            GetComponentInParent<MaintenanceRobotItem>().setIcon(itemIcon);
            playerbackpack.remove("Maintenance Protocol");
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
}
