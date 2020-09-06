using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    int score = 0;
    public int lives = 3;
    int maxLives = 5;


    // Update is called once per frame
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
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

    public int GetMaxLives()
    {
        return maxLives;
    }

    public void LoseLife()
    {
        lives--;
    }

    public void GainLife()
    {
        lives++;
    }

}
