using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    /// <summary>
    /// The current instance of the NPC.
    /// </summary>
    public GameObject currentNPC;

    /// <summary>
    /// The player game object that the agent should target/look for.
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// Keeps track of the amount of time passed for testing.
    /// </summary>
    public float elapsedTime = 0.0f;

    /// <summary>
    /// How much time is passed before the agent returns to "Patrol" state.
    /// </summary>
    public float targetTime = 5.0f;

    // Every child class will override this with their specific instance of "Test".
    public virtual bool Test()
    {
        return false;
    }
}
