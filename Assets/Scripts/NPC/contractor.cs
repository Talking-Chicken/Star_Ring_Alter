using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contractor : InteractiveObj
{
    [SerializeField] private string talkingNode;
    [SerializeField] string[] parts;
    PlayerBackpack playerBackpack;
    bool none = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interact()
    {

        PlayerControl player = FindObjectOfType<PlayerControl>();
        
          

            player.ChangeState(player.stateExplore);
            talkingNode = "contractor.1";
            player.talkToSelf(talkingNode);
            //anim.Play("rita_gaming");

       
      

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
                none = true;
                break;
            }
        }
        if (none == false)
        {
            for (var i = 0; i < random_conversation.lines.Length; i++)
            {
                parts = random_conversation.lines[i].Split(',');
                parts[3] = parts[3].Replace("\r", "");

                if (parts[3].Equals("dialogue") && parts[2].Equals("FALSE"))
                {
                    talkingNode = parts[0];

                    random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "TRUE" + "," + parts[3];
                    none = true;
                    break;
                }
            }
            CSV();
        }
    }

}
