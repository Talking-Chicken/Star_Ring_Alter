using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingModule : NeuroImplantApp
{
    public HackingModule()
    {
        base.appName = "hacking module";
        base.Description = "hacking things, found in black market";
        base.MemoryStorage = 4;
    }

    public override void interact()
    {
        Debug.Log("using hacking module");
    }
}
