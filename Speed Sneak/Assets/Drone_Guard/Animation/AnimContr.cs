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

        currentState = new State();

        currentState.SetUpState();

    }

    // Update is called once per frame
    void Update()
    {
        currentState.StateAction(gameObject, rotationDirection, anim);
    }


    void LateUpdate()
    {
        currentState.CheckPossibleStateChange(gameObject);
    }
}
