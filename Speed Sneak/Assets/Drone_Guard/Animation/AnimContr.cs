using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimContr : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public Vector3 NPCOriginalPosition;

    public List<Transition> transitions;

    public State currentState;
    void Start()
    {
        anim = GetComponent<Animator>();

        NPCOriginalPosition = gameObject.transform.position;

        currentState = new State();

        transitions = currentState.SetUpState(NPCOriginalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.StateAction(gameObject, anim);
        // TODO: Remove these onces the animations for the player and NPCs are down. These are here for reference.
        if (Input.GetKeyDown("s"))
        {
            anim.Play("ShutDown");
        }

        if (Input.GetKeyDown("w"))
        {
            anim.Play("WakeUp");
        }

        if (Input.GetKeyDown("d"))
        {
            anim.Play("Destroyed");
        }

    }


    void LateUpdate()
    {
        State newState = currentState.CheckPossibleStateChange(currentState, transitions, gameObject);
        currentState = newState;
    }
}
