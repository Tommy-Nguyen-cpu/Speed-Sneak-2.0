using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour
{
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
