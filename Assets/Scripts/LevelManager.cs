using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int par;
    public int score;
    public Text parText;
    public Text scoreText;

    void Start()
    {
        UpdateUI();
    }

    public void IncrementScore()
    {
        score++;
        UpdateUI();
    }

    public void UpdateUI()
    {
        parText.text = "Par: " + par;
        scoreText.text = "Score: " + score;
    }
}
