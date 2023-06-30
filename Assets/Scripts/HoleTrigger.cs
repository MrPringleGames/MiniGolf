using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleTrigger : MonoBehaviour
{
    public GameObject[] players; // Array of player GameObjects
    public Transform nextCourse; // Reference to the next course's transform
    public Scoreboard scoreboard; // Reference to the scoreboard script

    private int playersFinishedCount = 0; // Count of players who have finished the hole
    void OnTriggerEnter(Collider other)
    {
        // Check if the ball colliding with the hole belongs to a player
        if (other.CompareTag("Player"))
        {
            // Increment the count of players who have finished the hole
            playersFinishedCount++;

            // Check if all players have finished the hole
            if (playersFinishedCount == players.Length)
            {
                // Move the ball to the next course
                other.transform.position = nextCourse.position;

                // Save scores to the scoreboard
                for (int i = 0; i < players.Length; i++)
                {
                    PlayerScore playerScore = players[i].GetComponent<PlayerScore>();
                    scoreboard.AddScore(playerScore.playerName, playerScore.score);
                }

                // Reset the ball hit counter for all players
                foreach (GameObject player in players)
                {
                    PlayerScore playerScore = player.GetComponent<PlayerScore>();
                    playerScore.ResetScore();
                }

                // Reset the count of players who have finished the hole
                playersFinishedCount = 0;
            }
        }
    }
}
///Attach the HoleManager script to the hole GameObject in your scene. 
///Assign the appropriate values to the players array, nextCourse, and scoreboard variables in the inspector.

///Make sure each player has a PlayerScore script attached to their respective ball GameObjects. 
///The PlayerScore script should keep track of the player's score and provide a method to reset the score.

///The Scoreboard script should handle adding scores to the scoreboard.

///Modify the collision detection logic in the OnTriggerEnter method if needed, depending on how you set up your hole and ball collision detection in your game.


/*{
    public string nextLevelName;
    public float nextLevelDelay;
    public float endGameDelay;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            StartCoroutine(TriggerNextLevel());
        }
    }

    IEnumerator TriggerNextLevel()
    {
        yield return new WaitForSeconds(nextLevelDelay);
        if (nextLevelName != "")
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            yield return new WaitForSeconds(endGameDelay);
            Application.Quit();
        }
    }
}
*/