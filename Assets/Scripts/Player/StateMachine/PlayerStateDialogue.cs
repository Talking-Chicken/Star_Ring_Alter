using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDialogue : PlayerStateBase
{
    public override void EnterState(PlayerControl player) {
        player.UIControl.closeTime();
    }
    public override void UpdateState(PlayerControl player) {

    }
    public override void FixedupdateState(PlayerControl player) {

    }
    public override void LeaveState(PlayerControl player) {
        PrivoudState = this;
        player.UIControl.openTime();
    }
}
