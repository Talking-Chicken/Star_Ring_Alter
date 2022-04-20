using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rita : InteractiveObj
{
    [SerializeField] private string talkingNode;

    public string TalkingNode { get => talkingNode; set => talkingNode = value; }
    private void Update()
    {
        
    }
    public override void interact()
    {
        base.Player.ChangeState(Player.stateExplore);
        base.Player.talkToSelf(talkingNode);
    }
}
