using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    

    public Image[] lifeIcons;
    public Sprite spaceShip;
    

    /*private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1 )
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }*/


    // Start is called before the first frame update
    public void UpdateLifeCounter()
    {
        int lives = FindObjectOfType<GameSession>().GetLives();
        int maxLives = FindObjectOfType<GameSession>().GetMaxLives();

        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (i > maxLives)
            {
                lives = maxLives;
            }

            if (i < lives)
            {
                lifeIcons[i].sprite = spaceShip;
            }

            if (i < lives)
            {
                lifeIcons[i].enabled = true;
            }
            else
            {
                lifeIcons[i].enabled = false;
            }
        }
    }
}
