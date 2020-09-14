using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIcons : MonoBehaviour
{
    [SerializeField] bool extraLife;
    [SerializeField] bool speedShot;
    [SerializeField] bool shield;
    [SerializeField] float resetSpeedTime = 1f;
    [SerializeField] float newProjectileSpeed = 100f;
   
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
                Debug.Log(player.projectileSpeed);
                player.projectileSpeed = newProjectileSpeed;
                Debug.Log(player.projectileSpeed);
                ChangedProjectileSpeed();
                
            }
            else if (extraLife == true)
            {
                GameSession gameSession = FindObjectOfType<GameSession>();
                gameSession.GainLife();
                Destroy(gameObject);
            }
        }
    }


    private void ChangedProjectileSpeed()
    {
        Debug.Log("start coroutine");
        StartCoroutine(FastProjectileDuration());
        Debug.Log("end coroutine");
    }

    IEnumerator FastProjectileDuration()
    {
        Debug.Log("before reset");
        yield return new WaitForSeconds(resetSpeedTime);
        ResetProjectileSpeed();
        Debug.Log("After reset");
    }

    private void ResetProjectileSpeed()
    {
        Player player = FindObjectOfType<Player>();
        player.projectileSpeed = player.baseProjectileSpeed;

    }
}
