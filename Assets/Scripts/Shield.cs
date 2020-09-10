using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Player player;
    float moveSpeed;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    float newXPos;
    float newYPos;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        
    }
        

    // Update is called once per frame
    void Update()
    {
        xMin = player.xMin;
        xMax = player.xMax;
        yMin = player.yMin;
        yMax = player.yMax;
        moveSpeed = player.moveSpeed;
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer.isFriendlyShot == false)
            {
                if (!damageDealer) { return; }
                damageDealer.Hit();
                Destroy(gameObject);
            }
        else
            {
                return;
            }   
    }
}
