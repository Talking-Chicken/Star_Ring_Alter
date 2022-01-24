using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingModule : NeuroImplantApp
{
    public HackingModule()
    {
        base.appName = "hacking module";
        base.setDescription("hacking things, found in black market");
        base.setMemoryStorage(6);
    }

    public override void interact()
    {
        Debug.Log("using hacking module");
    }
}
