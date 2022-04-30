using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//player will select card and play card in this state
public class PlayerStatePlay : PlayerStateBaseArcade
{
    public override void EnterState(PlayerControlArcade player) {
        for (int i = 0; i < player.Hand.Count; i++) {
                player.Hand[i].GetComponent<EventTrigger>().enabled = true;
        }
    }
    public override void Update(PlayerControlArcade player) {
        if (player.CurrentCard == null) {
            player.Indicator.SetActive(false);
        } else {
            player.Indicator.SetActive(true);
            player.Indicator.transform.position = player.CurrentCard.transform.position;
        }
    }
    public override void LeaveState(PlayerControlArcade player) {
        for (int i = 0; i < player.Hand.Count; i++) {
                player.Hand[i].GetComponent<EventTrigger>().enabled = false;
        }
    }
}
