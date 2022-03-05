using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class OperatingSystemModule : NeuroImplantApp
{
    public OperatingSystemModule()
    {
        if (base.isOverride) {
            base.appName = "OS";
            base.Description = "the essential module that keeps Mr.Rabbit functional";
            base.MemoryStorage = 5;
        } else {
            base.appName = base.appName;
            base.Description = base.Description;
            base.MemoryStorage = base.MemoryStorage;
        }
    }
}
