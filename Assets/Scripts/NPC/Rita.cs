using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rita : InteractiveObj
{
    [SerializeField] private string talkingNode;
    [SerializeField] Animator anim;

    public string TalkingNode { get => talkingNode; set => talkingNode = value; }
    private void Update()
    {
        if (Time_text.time_2.Minute >= 20)
        {
            talkingNode = "Rita.2";
            anim.Play("rita_gaming");
        }
        else { talkingNode = "Rita.1"; }
    }
    public override void interact()
    {
        base.Player.ChangeState(Player.stateExplore);
        base.Player.talkToSelf(talkingNode);
    }
}
