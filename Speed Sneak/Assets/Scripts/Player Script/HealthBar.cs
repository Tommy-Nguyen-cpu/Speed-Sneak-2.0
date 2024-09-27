using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient healthBarColor;

    public Image healthBarFill;

    public void SetMaximumHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        // Sets health bar color to green.
        healthBarFill.color = healthBarColor.Evaluate(1f);
    }

    public void SetHealth(float increment)
    {
        slider.value += increment;
        healthBarFill.color = healthBarColor.Evaluate(slider.normalizedValue);

        if(slider.value == 0)
        {
            TitleScreen.currentState = TitleScreen.GameState.LOST;
            SceneManager.LoadScene(0);
        }
    }
}
