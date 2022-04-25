using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintenanceRobotItem : Item
{
    public MaintenanceRobotItem()
    {
        base.setName("Maintenance Robot");
        base.setDescription("Rabbit Heavy Industries's recent released third generation industrial robots.It has equipped with Maintenance Protocol and can fulfill the tasks of any Confederation Certified junior engineer.");
       // base.setDescriptionAfterUse("oh, you used it");
     
    }

    public override void useItem()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf("Response.robot");
    }
}
