using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperatingSystemModule : NeuroImplantApp
{
    public OperatingSystemModule()
    {
        base.appName = "OS";
        base.Description = "the essential module that keeps Mr.Rabbit functional";
        base.MemoryStorage = 5;
    }
}
