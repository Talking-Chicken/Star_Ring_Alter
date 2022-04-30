using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//set up the initial hand deck
public class PlayerStateSetUp : PlayerStateBaseArcade
{
    public override void EnterState(PlayerControlArcade player) {
        for (int i = 0; i < player.HandSize; i++) {
            player.draw(i);
        }

        player.ChangeState(player.statePlay);
    }
    public override void Update(PlayerControlArcade player) {

    }
    public override void LeaveState(PlayerControlArcade player) {

    }
}
