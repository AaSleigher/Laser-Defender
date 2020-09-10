using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIcons : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.gameObject.CompareTag("Shredder"))
        {
            Destroy(gameObject);
        }
        else
        {
            Player player = FindObjectOfType<Player>();
            player.ShieldUp();
            Destroy(gameObject);
        }
        
    }
}
