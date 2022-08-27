using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicMobController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 3;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("Player"))
        {
            TitleScreen.currentState = TitleScreen.GameState.LOST;
            SceneManager.LoadScene(0);
            return;
        }
        transform.Rotate(Vector3.up, 180);
    }
}
