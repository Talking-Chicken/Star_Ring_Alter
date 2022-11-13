using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : InteractiveObj
{
    [SerializeField] private string talkingNode;
   
    // Start is called before the first frame update
    void Start()
    {
    interactonly=true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interact()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf(talkingNode);
    }
}
