using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleTrigger : MonoBehaviour
{
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
