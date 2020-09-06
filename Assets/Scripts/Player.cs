﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float healtht = 200f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = .7f;
    

    [Header("Projectile")]
    [SerializeField] GameObject laserPreFab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Sounds")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = .25f;


    Coroutine firingCoroutine;


    int lives;



    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        GameSession gamesession = FindObjectOfType<GameSession>();
        lives = gamesession.lives;

    }

    float xMin;
    float xMax;
    float yMin;
    float yMax;
   

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        FindObjectOfType<Health>().UpdateLifeCounter();
        
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();
        FindObjectOfType<GameSession>().LoseLife();
        Die();
    }

    private void Die()
    {
        if ( lives > 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
            FindObjectOfType<Level>().LoadGameScene();
        }
        else if (lives <= 0)
        {
            
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
            FindObjectOfType<Level>().LoadGameOver();
        }
    }

    
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                     laserPreFab,
                     transform.position,
                     Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

   

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    
}
