# Speed-Speak-2.0

## Optimized FSM
Branch dedicated to optimize and test the FSM.
Previous version of the FSM was incredibly inefficient with multiple State classes instantiated and attached to the NPC as script objects.
Because Unity's garbage collect does not handle unused/discarded gameobjects/scripts well, the games performance can decrease immensely in the long run.

### New Format
The "State" class is now called by the NPCs controller ( **AnimContr.cs** ).

The AnimContr.cs file contains a list of all possible Transition with each Transition object containing a Condition object and a State object (which we call "target").
