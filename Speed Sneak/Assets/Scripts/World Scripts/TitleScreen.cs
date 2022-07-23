using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    public enum GameState
    {
<<<<<<< Updated upstream
        WON, LOST, NEITHER
    }

    public static GameState currentState = GameState.NEITHER;

    void Awake()
    {
        Debug.Log("Script woke up!");
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
=======
>>>>>>> Stashed changes
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
