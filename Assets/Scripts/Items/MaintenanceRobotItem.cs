using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintenanceRobotItem : Item
{
    public MaintenanceRobotItem()
    {
        base.setName("maintenance robot");
        base.setDescription("it's the robot for maintenance");
        base.setDescriptionAfterUse("oh, you used it");
    }

    public override void useItem()
    {
        
    }
}
