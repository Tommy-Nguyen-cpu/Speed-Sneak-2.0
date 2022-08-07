using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    /// <summary>
    /// Determines whether we display a "Win" or "Lose" text (or none).
    /// </summary>
    public enum GameState
    {
        WON, LOST, NEITHER
    }

    /// <summary>
    /// Will be set everytime the player loses/wins.
    /// </summary>
    public static GameState currentState = GameState.NEITHER;

    /// <summary>
    /// Will activate each time we switch to this scene.
    /// </summary>
    void Awake()
    {
        if(currentState == GameState.WON)
        {
            Text statusText = GameObject.Find("PlayerWinOrLose").GetComponent<Text>();
            statusText.enabled = true;
            statusText.color = Color.green;
            statusText.text = "YOU WON!";
        }
        else if(currentState == GameState.LOST)
        {
            Text statusText = GameObject.Find("PlayerWinOrLose").GetComponent<Text>();
            statusText.enabled = true;
            statusText.color = Color.red;
            statusText.text = "You lost...";
        }
    }
    /// <summary>
    /// Switches the scene to the "SampleScene" when the button is clicked.
    /// </summary>
    public void SwitchTheClass()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
