using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base state class for player state
public abstract class PlayerStateBaseArcade 
{
    public abstract void EnterState(PlayerControlArcade player);
    public abstract void Update(PlayerControlArcade player);
    public abstract void LeaveState(PlayerControlArcade player);
}
