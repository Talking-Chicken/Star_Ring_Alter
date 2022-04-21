using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rita : InteractiveObj
{
    [SerializeField] private string talkingNode;
    [SerializeField] Animator anim;

    bool once;

   [SerializeField] string[] parts;
    public string TalkingNode { get => talkingNode; set => talkingNode = value;
    }

    private void Start()
    {
        once = true;
        CSV();

    }
    private void Update()
    {
  
        if (Time_text.time_2.Minute >= 20)
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
            if (parts[3].Equals("dialogue"))
                Debug.Log(i + " is equals to dialogue node");
            if (parts[2].Equals("TRUE"))
                Debug.Log(i + " is equal to true");

            if (parts[3].Equals("dialogue") && parts[2].Equals("TRUE"))
            {
                talkingNode = parts[0];
                
                random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "FALSE" + "," + parts[3];
               break;
            }
        }

    }
}
