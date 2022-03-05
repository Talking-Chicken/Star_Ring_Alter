using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingModule : NeuroImplantApp
{
    public HackingModule()
    {
        if (!base.isOverride) {
            base.appName = "hacking module";
            base.Description = "hacking things, found in black market";
            base.MemoryStorage = 3;
        }
    }

    public override void interact()
    {
        Debug.Log("using hacking module");
    }
}
