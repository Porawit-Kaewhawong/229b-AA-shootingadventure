using UnityEngine;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    void Start()
    {
        UpdateUI();
    }

    public void AddScore()
    {
        score++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}