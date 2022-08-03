using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimContr : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public Vector3 NPCOriginalPosition;

    public List<Transition> transitions;

    public char rotationDirection;

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
        currentState.StateAction(gameObject, rotationDirection, anim);
    }


    void LateUpdate()
    {
        State newState = currentState.CheckPossibleStateChange(currentState, transitions, gameObject);
        currentState = newState;
    }
}
