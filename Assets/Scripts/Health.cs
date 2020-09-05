using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] Image[] lifeIcon;
    public int lives = 3;
    int maxLives = 5;


    // Start is called before the first frame update
    void Update()
    {
        for (int i = 0; i < lifeIcon.Length; i++)
        {
            if (i > maxLives)
            {
                i = maxLives;
            }

            if (i < lives)
            {
                lifeIcon[i].enabled = true;
            }
            else
            {
                lifeIcon[i].enabled = false;
            }
        }
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
