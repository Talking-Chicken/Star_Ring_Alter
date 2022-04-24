using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rita : InteractiveObj
{
    [SerializeField] private string talkingNode;
    [SerializeField] Animator anim;

    bool once;

   [SerializeField] string[] parts;
    public string TalkingNode { get => talkingNode; set => talkingNode = value;}

    private void Start()
    {
        once = true;
        CSV();

    }
    private void Update()
    {
  
        if (Time_text.time_2.Minute >= 21)
        {
            talkingNode = "Rita.2";
            anim.Play("rita_gaming");
            
        }

        else {
           
            if (once) { 
               // CSV();
                
                once = false;
            }
        
        }
    }
    public override void interact()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf(talkingNode);
    }

    void CSV()
    {

        Debug.Log("test");
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[3] = parts[3].Replace("\r", "");

            if (parts[3].Equals("dialogue") && parts[2].Equals("TRUE"))
            {
                talkingNode = parts[0];
                
                random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "FALSE" + "," + parts[3];
               break;
            }
        }
    }

    public override void useItem()
    {
        //get item first
        Item deliveryItem = InventoryGUIControl.currentUnit.items.Dequeue();
        if (deliveryItem.ItemName.ToLower().Trim().Contains("gear"))
            TalkingNode = "Rita.React_Gear";
        else if(deliveryItem.ItemName.ToLower().Trim().Contains("access card"))
            TalkingNode = "Rita.React_Access";
        else
            TalkingNode = "Rita.React_Other";

        //trigger rita's reaction dialogue to the item    
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf(TalkingNode);
    }

    public override void useNeuroImplant()
    {
        NeuroImplantApp app = NeuroGUIControl.currentUnit.NeuroApp;
        if (app.GetComponent<OperatingSystemModule>() != null)
            TalkingNode = "Rita.React_OS_Module";
        else
            TalkingNode = "Rita.React_Other_Module";
        
        //trigger Rita's reaction dialogue to the neuro app
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf(TalkingNode);
    }
}
