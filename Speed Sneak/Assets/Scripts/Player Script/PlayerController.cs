using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Ethan will implement the playing "Standing" animation if no buttons are pressed.
        // TODO: Work on allowing the player to move and apply animation accordingly.
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.Play("RunAndAim");
            transform.Translate(Vector3.forward * Time.deltaTime * (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) ? 20 : 1));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.Play("RunAndAim");
            transform.Translate(Vector3.back * Time.deltaTime * (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) ? 20 : 1));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -1);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, 1);
        }

    }
}
