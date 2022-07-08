using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimContr : MonoBehaviour
{
    // Start is called before the first frame update
    public static Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();

        // Enable "State" script so that we can start the FSM algorithm.
        GetComponent<State>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

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
}
