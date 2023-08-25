using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private TextMeshProUGUI restartText;

    [SerializeField]
    private Image[] livesRemaining;

    private int scorePoints;

    private int maximumLives = 3;

    public int currentLives = 3;

    public void Start()
    {
        Physics.IgnoreLayerCollision(7, 7);
        SetText(false);
    }

    public void Update()
    {
        scoreText.text = "Score: " + scorePoints;
        DisplayCurrentLives();
        if(currentLives <= 0)
        {
            SetText(true);
        }
    }

    private void SetText(bool x)
    {
        gameOverText.enabled = x;
        restartText.enabled = x;
    }

    private void DisplayCurrentLives()
    {
        if(currentLives < 0)
        {
            return;
        }
        for (int i = 0 ; i < currentLives ; i++)
        {
            livesRemaining[i].enabled = true;
        }
        for(int i = currentLives ; i < maximumLives ; i++)
        {
            livesRemaining[i].enabled = false;
        }
    }

    public void AddScore(int points)
    {
        scorePoints += points;
    }

    public void RemoveLife(int damage)
    {
        currentLives -= damage;
    }
}
