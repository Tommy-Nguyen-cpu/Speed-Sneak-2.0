using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCondition : Condition
{
    /// <summary>
    /// If nothing is happening or if it has been a couple of seconds since something has happened, then the agent will go back to patrolling (go to "patrol" state).
    /// </summary>
    /// <returns></returns>
    public override bool Test()
    {
        // There's a very likely change that elapsedTime will have a higher number than targetTime, so we'll check to see if elapsedTime is greater than or equal to targetTime.
        // This possibility is increased because patrol time is also checked last (which should remain the case).
        if(elapsedTime >= targetTime)
        {
            return true;
        }
        return false;
    }
}
