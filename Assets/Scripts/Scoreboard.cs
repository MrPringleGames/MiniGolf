using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public GameObject scoreboard; // Reference to the scoreboard GameObject
    public GameObject gameOptionsPanel; // Reference to the game options panel GameObject

    private bool isShowingOptions = false; // Flag to track if the game options are currently shown

    void Update()
    {
        // Toggle game options on escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isShowingOptions)
            {
                // Show the scoreboard and game options panel
                scoreboard.SetActive(true);
                gameOptionsPanel.SetActive(true);
                Time.timeScale = 0f; // Pause the game
            }
            else
            {
                // Hide the scoreboard and game options panel
                scoreboard.SetActive(false);
                gameOptionsPanel.SetActive(false);
                Time.timeScale = 1f; // Resume normal game speed
            }

            isShowingOptions = !isShowingOptions;
        }
    }
}
///With this updated script, the scoreboard and game options panel will be shown when the escape key is pressed for the first time.
///Pressing the escape key again will hide them, and the game will resume with normal controls and speed.

///Make sure to assign the appropriate references to the scoreboard and gameOptionsPanel variables in the inspector, 
///and set up your UI elements and functionality accordingly.

