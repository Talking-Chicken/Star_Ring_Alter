using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineeringModule : NeuroImplantApp
{
    public EngineeringModule()
    {
        base.appName = "engineering module";
        base.Description = "to build, fix, and disassemble";
        base.MemoryStorage = 4;
    }

    public override void interact()
    {
        Debug.Log("using engineering module");
    }
}
