using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    public int score;
    public int lives = 3;
    public int maxLives = 5;

    // Update is called once per frame
    private void Awake()
    {
        SetUpSingleton();
    }


    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
            score = GetScore();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
                    {
        score += scoreValue;
      }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public int GetLives()
    {
        return lives;
    }

    public void LoseLife()
    {
        lives--;
        Respawn();
    }

    public void GainLife()
    {
        lives++;
        if (lives > maxLives)
        {
            lives = maxLives;
        }
    }

    public void Respawn()
    {
        if (lives > 0)
        {
            FindObjectOfType<Level>().RestartLevel();
        }
        else if (lives <= 0)
        {
            FindObjectOfType<Level>().LoadGameOver();
        }
    }

}
