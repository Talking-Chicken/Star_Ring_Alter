using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineeringModule : NeuroImplantApp
{
    public EngineeringModule()
    {
        base.appName = "engineering module";
        base.setDescription("to build, fix, and disassemble");
        base.setMemoryStorage(7);
    }

    public override void interact()
    {
        Debug.Log("using engineering module");
    }
}
