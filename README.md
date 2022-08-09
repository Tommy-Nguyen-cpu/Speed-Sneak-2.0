# Speed-Speak-2.0

## Running The Game
There are 2 options for running the program, you can either run via the executables found in "Executables (Windows and MacOs) or via the actual Unity projects folder.

### Running with Executables
There are 2 types of executables found, an executable for Windows systems (.exe) and an executable for MacOs (.dmg). Both files can be ran by click on the respective executables twice. The executables can be found in this directive (Wherever you unzipped the projects folder)/Speed-Sneak-2.0/Executables (Windows and MacOs).

### Running with Unity Projects Folder
In order to run the actual projects folder, you will need to have Unity3D already installed. Unity and Unity Hub (the lobby for projects) can be downloaded through this link: (https://unity3d.com/get-unity/download?%20ga=2.41101). It is preferable to have the most recent Unity version (2021 or greater).

Once Unity3D is done downloading and you have the correct Unity version follow these steps to get the program running:
1. Open "Unity Hub"
2. In the top right corner of the screen in Unity Hub, you should see a button called "Open", click on that.
3. Once you do, navigate to the folder for "Speed-Sneak". "Speed-Sneak" should be in a folder called "Speed-Sneak-2.0" (It may also be called "Speed-Sneak-2.0-main").
4. Once you open the folder, everything should work properly. Now, you should be able to run the program simply by clicking the play button.


## Optimized FSM
Branch dedicated to optimize and test the FSM.
Previous version of the FSM was incredibly inefficient with multiple State classes instantiated and attached to the NPC as script objects.
Because Unity's garbage collect does not handle unused/discarded gameobjects/scripts well, the games performance can decrease immensely in the long run.

### New Format
The "State" class is now called by the NPCs controller ( **AnimContr.cs** ).

The AnimContr.cs file contains a list of all possible Transition with each Transition object containing a Condition object and a State object (which we call "target").

```

public class Transition
{
    /// <summary>
    /// Conditional that should be met change states.
    /// </summary>
    public Condition conditional;

    /// <summary>
    /// Target state if condition is met.
    /// </summary>
    public State target;
}

```
*Transition class that contains objects to the Condition and State class. Condition class checks to see if the current environment meets the condition required to switch to the "target" state.*
