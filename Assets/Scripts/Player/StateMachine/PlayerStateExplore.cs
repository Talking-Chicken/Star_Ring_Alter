using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateExplore : PlayerStateBase
{
    public override void EnterState(PlayerControl player) {

    }
    public override void UpdateState(PlayerControl player) {
        if (PlayerControl.canTalk)
        {
            if (Input.GetKeyDown(player.KeyManager.talk) && !player.dialogueRunner.IsDialogueRunning)
                player.talkToNPC();
        }

        if (player.detectInteractiveObj() && Input.GetKeyDown(player.KeyManager.interact)) {
            player.interact(player.detectingObj);
        }
    }
    public override void FixedupdateState(PlayerControl player) {

    }
    public override void LeaveState(PlayerControl player) {
        PrivoudState = this;
    }
}
