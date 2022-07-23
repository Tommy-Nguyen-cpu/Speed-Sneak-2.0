using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            anim.Play("Standing");
        }

    }

}
