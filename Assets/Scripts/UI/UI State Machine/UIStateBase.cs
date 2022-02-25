using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIStateBase
{
    public abstract void EnterState(UIControl UI);
    public abstract void UpdateState(UIControl UI);
    public abstract void LeaveState(UIControl UI);
}
