using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public int score;
    public Text scoreText;
    private HoleTrigger holeManager;

    private void Start()
    {
        holeManager = FindObjectOfType<HoleTrigger>();
        UpdateScoreText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            score++;
            UpdateScoreText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hole"))
        {
            holeManager.UpdateScore(score);
            ResetScore();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}
///In this script, the PlayerScore class keeps track of the player's score and updates the score text in the UI. 
///It also interacts with the HoleManager script when the player reaches the hole, updating the total score and resetting the player's score.

///To use this script, follow these steps:

///Create a Text UI element for the score display and assign it to the scoreText variable in the Inspector of the player object.
///Attach the PlayerScore script to the player object in the scene.
///Make sure you have a HoleManager script attached to the hole object or a dedicated game manager object.
///Tag the obstacle GameObjects with the tag "Obstacle" so that the script can detect collisions with them.
///Tag the hole GameObject with the tag "Hole" so that the script can detect when the player reaches it.
///Now, when the player collides with an obstacle, the score will increment and update in the UI. 
///When the player reaches the hole, the PlayerScore script will communicate with the HoleManager script to update the total score and reset the player's score.