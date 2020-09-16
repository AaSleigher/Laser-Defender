using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    GameSession gameSession;

    int maxLives = 5;
    int lives;

    public Image[] hearts;
    public Sprite fullHeart;


    // Start is called before the first frame update
    public void Update()
    {
        maxLives = 5;
        lives = FindObjectOfType<GameSession>().GetLives();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i > lives)
            {
                maxLives = lives;
            }

            if (i < maxLives)
            {
                hearts[i].sprite = fullHeart;
            }

            if (i < lives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public int GetMaxLives()
    {
        return maxLives;
    }



}
