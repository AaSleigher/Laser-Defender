using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIcons : MonoBehaviour
{
    [SerializeField] bool extraLife;
    [SerializeField] bool speedShot;
    [SerializeField] bool shield;
    [SerializeField] public float resetFiringTime = 5f;
    [SerializeField] float newFiringPeriod = .05f;
   
    private void OnTriggerEnter2D(Collider2D collision)

     {
        if (collision.gameObject.CompareTag("Shredder"))
        {
            Destroy(gameObject);
        }
        else
        {
            if (shield == true)
            {
                Player player = FindObjectOfType<Player>();
                player.ShieldUp();
                Destroy(gameObject);
            }
            else if (speedShot == true)
            {
                Player player = FindObjectOfType<Player>();
                player.projectileFiringPeriod = newFiringPeriod;
                player.ChangedFiringPeriod();
                Destroy(gameObject);
                
            }
            else if (extraLife == true)
            {
                GameSession gameSession = FindObjectOfType<GameSession>();
                gameSession.GainLife();
                Destroy(gameObject);
            }
        }
    }


    
}
