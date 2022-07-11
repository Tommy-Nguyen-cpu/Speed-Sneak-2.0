﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    /// <summary>
    /// The current state the guard is in.
    /// </summary>
    public States currentState;

    /// <summary>
    /// Lists of possible transitions so the guard knows which states are available.
    /// </summary>
    public List<Transition> transitions;

    /// <summary>
    /// Keeps track of how much time has passed in order to check to see if we should change to "Patrol" state.
    /// </summary>
    public float currentTime = 0.0f;


    private PatrolMovement patrolling;

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

    // Called when the current instance of the script is loaded.
    void Awake()
    {
        Player = GameObject.Find("Player");

        // By default, the agent should be patrolling the field.
        setState(States.PATROL);

        // Agent's controller (AnimContr.cs) will enable the script.
        this.enabled = false;

        transitions = new List<Transition>();

        // Set up transitions.

        // "Patrol" condition should be the last thing it checks.
        // Sets up the "patrol" transition.
        Transition toPatrol = new Transition();
        toPatrol.conditional = new PatrolCondition();
        transitions.Add(toPatrol);

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
    }

    // Called when the current instance of the script is enabled.
    void OnEnable()
    {
        // Initializes the "PatrolMovement" which allows the NPC to patrol in a circular motion.
        patrolling = new PatrolMovement();

        // Sets the original position of the NPC.
        patrolling.NPCOriginalPosition = AnimContr.NPCOriginalPosition;


        // Initialize the states
        State patrol = gameObject.AddComponent<State>() as State;
        patrol.currentState = States.PATROL;
        transitions[0].target = patrol;

        State suspect = gameObject.AddComponent<State>() as State;
        suspect.currentState = States.SUSPECT;
        transitions[1].target = suspect;

        State win = gameObject.AddComponent<State>() as State;
        win.currentState = States.WIN;
        transitions[2].target = win;


        State chase = gameObject.AddComponent<State>() as State;
        chase.currentState = States.CHASE;
        transitions[3].target = chase;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentState == States.CHASE)
        {
            // TODO: Implement behaviour for chase state.
            Debug.Log("Guard is chasing the player!");

            // TODO: Temporary action agent will take. Will change once we implement PCG and A* Search.
            // Checks to see if we finished the "WakeUp" animation.
            if (AnimContr.anim.GetCurrentAnimatorStateInfo(0).IsName("DroneGuard|Idle"))
            {
                //gameObject.transform.LookAt(Player.transform, Vector3.forward);
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * 5);
            }
        }
        else if(currentState == States.PATROL)
        {
            // TODO: Implement behaviour for patrol state.
            Debug.Log("Guard is patrolling!");

            // Checks to see if we finished the "WakeUp" animation.
            if(AnimContr.anim.GetCurrentAnimatorStateInfo(0).IsName("DroneGuard|Idle"))
            {
                patrolling.NPCPatrol(gameObject);
                // TODO: Temporary action agent will take. Will change once we implement PCG and A* Search.
                // When having the agent look at the player, we need to use Vector3.up and not Vector3.forward (strange distortion of guard asset).
                //gameObject.transform.LookAt(Player.transform, Vector3.up);
                //transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * 5);
            }
        }
        else if(currentState == States.SUSPECT)
        {
            Debug.Log("Something suspicious just happened!");
            if (AnimContr.anim.GetCurrentAnimatorStateInfo(0).IsName("DroneGuard|Idle"))
            {
                // DONE: Temporary action agent will take. Will change once we implement PCG and A* Search.
                gameObject.transform.LookAt(SoundDetection.sourceOfSound.transform, Vector3.up);
                transform.position = Vector3.MoveTowards(transform.position, SoundDetection.sourceOfSound.transform.position, Time.deltaTime * 5);

                // We will disable the flag if the agent reached the location of the sound.
                if (Vector3.Distance(transform.position, SoundDetection.sourceOfSound.transform.position) == 0)
                {
                    SoundDetection.soundDetected = false;

                }
            }

        }
        else if(currentState == States.WIN)
        {
            // TODO: Implement behaviour for win state. Should literally just bring up a menu saying "player win" or "player lost".
            Debug.Log("Player came into contact with guard! Player lost!");
        }

    }

    // Like Update, but occurs at the end of a frame.
    void LateUpdate()
    {

        foreach(Transition transition in transitions)
        {
            // Set values for Condition fields.
            transition.conditional.Player = Player;
            transition.conditional.currentNPC = gameObject;
            transition.conditional.elapsedTime = currentTime;

            // Keeps track of the next state.
            States nextState = transition.target.currentState;
            if (transition.conditional.Test())
            {
                transition.target.enabled = true;

                // Passes the current timeCounter value to the next state so that the agent continues to patrol smoothly.
                transition.target.patrolling.timeCounter = patrolling.timeCounter;


                // Reset timer.
                transition.conditional.elapsedTime = 0.0f;

                this.enabled = false;

                // Removes the current script from the guard.
                Destroy(this);

                // Removes all "State" scripts that are not the script we just enabled.
                foreach(Transition t in transitions)
                {
                    if(t.target.currentState != nextState)
                    {
                        Destroy(t.target);
                    }
                }
                return;
            }
        }
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
