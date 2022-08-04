using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            rotatePlayer(KeyCode.UpArrow, 0f);
            anim.Play("RunAndAim");
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rotatePlayer(KeyCode.DownArrow, 180f);
            anim.Play("RunAndAim");
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.Play("RunAndAim");
            rotatePlayer(KeyCode.LeftArrow, -90f);
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.Play("RunAndAim");
            rotatePlayer(KeyCode.RightArrow, 90f);
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
        }

        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            anim.Play("Standing");
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("Goal"))
        {
            SceneManager.LoadScene(1);
            return;
        }
    }


    #region

    /// <summary>
    /// Keeps track of which key was previously pressed.
    /// </summary>
    private KeyCode? previousKeyPressed = null;

    /// <summary>
    /// Keeps track of previous orientation of the player.
    /// </summary>
    private float previousRotation = 0;

    /// <summary>
    /// Rotates the player.
    /// </summary>
    /// <param name="pressedKey"></param>
    /// <param name="rotationDirection"></param>
    private void rotatePlayer(KeyCode? pressedKey, float rotationDirection)
    {
        if(previousKeyPressed == null || previousKeyPressed != pressedKey)
        {
            // Resets the previous rotation so that the player faces forward again.
            transform.Rotate(Vector3.up, (-1 * previousRotation));
            previousKeyPressed = pressedKey;
            previousRotation = rotationDirection;

            // Rotates in the new direction.
            transform.Rotate(Vector3.up, rotationDirection);
        }
    }

    #endregion

}
