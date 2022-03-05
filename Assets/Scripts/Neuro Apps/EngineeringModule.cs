using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineeringModule : NeuroImplantApp
{
    public EngineeringModule()
    {
        if (!base.isOverride) {
            base.appName = "engineering module";
            base.Description = "to build, fix, and disassemble";
            base.MemoryStorage = 3;
        }
    }

    public override void interact()
    {
        Debug.Log("using engineering module");
    }
}
