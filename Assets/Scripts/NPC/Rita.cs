using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rita : InteractiveObj
{
    [SerializeField] private string talkingNode;
    [SerializeField] Animator anim;
    
    bool once=true;

   [SerializeField] string[] parts;
    public string TalkingNode { get => talkingNode; set => talkingNode = value;
    }

    private void Start()
    {
   
     
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
                CSV();
                
                once = false;
            }
        
             }
    }
    public override void interact()
    {
        base.Player.ChangeState(Player.stateExplore);
        base.Player.talkToSelf(talkingNode);
    }

    void CSV()
    {

        Debug.Log("test");
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(","[0]);
            if (parts[3] == "dialogue node" && parts[2]=="FALSE")
            {
                talkingNode = parts[0];
                
                random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "TRUE" + "," + parts[3];
               
            }
        }

    }
}
