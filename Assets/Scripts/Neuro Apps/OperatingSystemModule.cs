using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatingSystemModule : NeuroImplantApp
{
    public OperatingSystemModule()
    {
        base.appName = "OS";
        base.Description = "the essential module that keeps Mr.Rabbit functional";
        base.MemoryStorage = 3;
    }
}
