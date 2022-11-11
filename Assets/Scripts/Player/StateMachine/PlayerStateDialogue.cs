using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDialogue : PlayerStateBase
{
    
    public override void EnterState(PlayerControl player) {
        player.CanUI = false;
    }
    public override void UpdateState(PlayerControl player) {
        player.DialogueControl.displayName();
        player.DialogueControl.continueDialogue();
        player.DialogueControl.switchOption();
        player.adjuster.set_direction();
        player.DialogueControl.chooseOption(player.DialogueControl.currentOption);
    }
    public override void FixedupdateState(PlayerControl player) {

    }
    public override void LeaveState(PlayerControl player) {
        player.CanUI = true;
    }
}
