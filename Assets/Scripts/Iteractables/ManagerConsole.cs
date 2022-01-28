using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerConsole : InteractiveObj
{
    public bool isElevatorActivated;
    public override void interact()
    {
        isElevatorActivated = true;
    }
}
