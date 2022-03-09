using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateExplore : PlayerStateBase
{
    public override void EnterState(PlayerControl player) {
        player.CanMove = true;
    }
    public override void UpdateState(PlayerControl player) {
        if (PlayerControl.canTalk)
        {
            if (Input.GetKeyUp(player.KeyManager.talk) && !player.dialogueRunner.IsDialogueRunning)
                player.talkToNPC();
        }

        if (player.detectInteractiveObj() && Input.GetKeyUp(player.KeyManager.interact)) {
            player.interact(player.DetectingObj);
        }
    }
    public override void FixedupdateState(PlayerControl player) {
        
    }
    public override void LeaveState(PlayerControl player) {
        player.CanMove = false;
    }
}
