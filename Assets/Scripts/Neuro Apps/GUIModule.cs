using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIModule : NeuroImplantApp
{
    public GUIModule() {
        if (base.isOverride) {
            base.appName = base.appName;
            base.Description = base.Description;
            base.MemoryStorage = base.MemoryStorage;
        } else {
            base.appName = "GUI module";
            base.Description = "See the World";
            base.MemoryStorage = 4;
        }
    }
}
