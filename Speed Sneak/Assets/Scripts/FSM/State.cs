using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State
{
    /// <summary>
    /// The current state the guard is in.
    /// </summary>
    public States currentState;

    /// <summary>
    /// Keeps track of how much time has passed in order to check to see if we should change to "Patrol" state.
    /// </summary>
    public float currentTime = 0.0f;

    // Initializes the "PatrolMovement" which allows the NPC to patrol in a circular motion.
    private PatrolMovement patrolling = new PatrolMovement();

    /// <summary>
    /// PATROL - if agent doesn't see player, follow a set route.
    /// CHASE - if agent sees player, chase player.
    /// WIN - if agent collides with the player, it is an automatic win.
    /// SUSPECT - if the player makes some noise within a specific distance of the agent, the agent will go to the location of the noise and check it out.
    /// </summary>
    public enum States
    {
        PATROL, CHASE, WIN, SUSPECT
    }

    // Keeps track of the player.
    GameObject Player;

    public State()
    {
        // Each time a new State is instantiated, we will find the player gameobject and set the "Player" field to be this gameobject.
        Player = GameObject.Find("Player");

    }

    // Called when the current instance of the script is loaded.
    public List<Transition> SetUpState()
    {

        // By default, the agent should be patrolling the field.
        setState(States.PATROL);

        List<Transition> transitions = new List<Transition>();

        // Set up transitions.

        // Sets up the "suspect" transition.
        Transition toSuspect = new Transition();
        toSuspect.conditional = new SuspectCondition();
        transitions.Add(toSuspect);

        // Sets up the "win" condition.
        Transition toWin = new Transition();
        toWin.conditional = new WinCondition();
        transitions.Add(toWin);


        // Sets up the "chase" transition.
        Transition toChase = new Transition();
        toChase.conditional = new ChaseCondition();
        transitions.Add(toChase);


        // "Patrol" condition should be the last thing it checks.
        // Sets up the "patrol" transition.
        Transition toPatrol = new Transition();
        toPatrol.conditional = new PatrolCondition();
        transitions.Add(toPatrol);


        // Sets the original position of the NPC.
        patrolling.NPCOriginalPosition = AnimContr.NPCOriginalPosition;


        State suspect = new State();
        suspect.currentState = States.SUSPECT;
        transitions[0].target = suspect;

        State win = new State();
        win.currentState = States.WIN;
        transitions[1].target = win;


        State chase = new State();
        chase.currentState = States.CHASE;
        transitions[2].target = chase;


        // Initialize the states
        State patrol = new State();
        patrol.currentState = States.PATROL;
        transitions[3].target = patrol;

        return transitions;
    }

    // Update is called once per frame
    public void StateAction(GameObject currentNPC)
    {
        // Checks to see if the guard animation is "WakeUp".
        if (AnimContr.anim.GetCurrentAnimatorStateInfo(0).IsName("DroneGuard|Idle"))
        {
            currentTime += Time.deltaTime;

            if (currentState == States.CHASE)
            {
                // TODO: Implement behaviour for chase state.
                Debug.Log("Guard is chasing the player!");

                // TODO: Temporary action agent will take. Will change once we implement PCG and A* Search.

                currentNPC.transform.LookAt(Player.transform, Vector3.up);
                currentNPC.transform.position = Vector3.MoveTowards(currentNPC.transform.position, Player.transform.position, Time.deltaTime * 5);
            }
            else if (currentState == States.PATROL)
            {
                // TODO: Implement behaviour for patrol state.
                Debug.Log("Guard is patrolling!");

                patrolling.NPCPatrol(currentNPC);
                // TODO: Temporary action agent will take. Will change once we implement PCG and A* Search.
                // When having the agent look at the player, we need to use Vector3.up and not Vector3.forward (strange distortion of guard asset).
                //gameObject.transform.LookAt(Player.transform, Vector3.up);
                //transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * 5);
            }
            else if (currentState == States.SUSPECT)
            {
                Debug.Log("Something suspicious just happened!");
                // DONE: Temporary action agent will take. Will change once we implement PCG and A* Search.
                currentNPC.transform.LookAt(SoundDetection.sourceOfSound.transform, Vector3.up);
                currentNPC.transform.position = Vector3.MoveTowards(currentNPC.transform.position, SoundDetection.sourceOfSound.transform.position, Time.deltaTime * 5);

                // We will disable the flag if the agent reached the location of the sound.
                if (Vector3.Distance(currentNPC.transform.position, SoundDetection.sourceOfSound.transform.position) == 0)
                {
                    SoundDetection.soundDetected = false;

                }

            }
            else if (currentState == States.WIN)
            {
                Debug.Log("Player came into contact with guard! Player lost!");
                TitleScreen.currentState = TitleScreen.GameState.LOST;
                SceneManager.LoadScene(0);
            }
        }

    }

    // Like Update, but occurs at the end of a frame.
    public State CheckPossibleStateChange(State currentState, List<Transition> transitions, GameObject currentNPC)
    {

        foreach(Transition transition in transitions)
        {
            // Set values for Condition fields.
            transition.conditional.Player = Player;
            transition.conditional.currentNPC = currentNPC;
            transition.conditional.elapsedTime = currentTime;

            if (transition.conditional.Test())
            {

                // Passes the current timeCounter value to the next state so that the agent continues to patrol smoothly.
                //transition.target.patrolling.timeCounter = patrolling.timeCounter;

                // Reset timer.
                currentTime = 0.0f;
                transition.conditional.elapsedTime = 0.0f;

                return transition.target;
            }
        }
        return currentState;
    }

    #region Helper Methods
    /// <summary>
    /// Method that helps in changing "currentState" for State class.
    /// </summary>
    /// <param name="state"></param>
    private void setState(States state)
    {
        this.currentState = state;
    }

    #endregion
}
