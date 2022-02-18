using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase
{
    public static PlayerStateBase PrivoudState{get; set;}
    public abstract void EnterState(PlayerControl player);
    public abstract void UpdateState(PlayerControl player);
    public abstract void FixedupdateState(PlayerControl player);
    public abstract void LeaveState(PlayerControl player);
}
