using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SuspectCondition : Condition
{
    /// <summary>
    /// If the agent hears a sound that the player made (i.e. stepped on broken glass) then the agent will go to the "suspect" state.
    /// </summary>
    /// <returns></returns>
    public override bool Test()
    {
        // Detects if the x and z coordinates are within earshot range of the agent.
        double differenceX = Math.Abs(Player.transform.position.x - currentNPC.transform.position.x);
        double differenceZ = Math.Abs(Player.transform.position.z - currentNPC.transform.position.z);

        // In our case, earshot for an agent would be within 100 distance from the player.
        if (SoundDetection.soundDetected && differenceX >= 100 && differenceZ >= 100)
        {
            return true;
        }
        return false;
    }
}
