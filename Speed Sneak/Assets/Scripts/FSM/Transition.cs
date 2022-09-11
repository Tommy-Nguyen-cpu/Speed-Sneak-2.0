using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    /// <summary>
    /// Conditional that should be met change states.
    /// </summary>
    public Condition conditional;

    /// <summary>
    /// Target state if condition is met.
    /// </summary>
    public State.States target;
}
