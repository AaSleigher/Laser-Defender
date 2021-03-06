﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Header("Player")]
    [SerializeField] public float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float healtht = 200f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = .7f;
    

    [Header("Projectile")]
    [SerializeField] GameObject laserPreFab;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] public float baseFiringPeriod = .5f;
    [SerializeField] public float projectileFiringPeriod = 0.1f;

    [Header("PowerUps")]
    [SerializeField] GameObject shieldPowerUp;

    [Header("Sounds")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = .25f;


    Coroutine firingCoroutine;

    GameSession gamesession;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        gamesession = FindObjectOfType<GameSession>();

    }

    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;


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
        FindObjectOfType<GameSession>().LoseLife();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
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


    public void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public void ShieldUp()
    {
        shieldPowerUp = Instantiate(
            shieldPowerUp,
            transform.position,
            Quaternion.identity) as GameObject;
    }

    public void ChangedFiringPeriod()
    {
        StartCoroutine(FastProjectileDuration());
    }

    IEnumerator FastProjectileDuration()
    {
        PowerUpIcons powerUp = FindObjectOfType<PowerUpIcons>();
        yield return new WaitForSeconds(powerUp.resetFiringTime);
        ResetFiringPeriod();
    }

    private void ResetFiringPeriod()
    {
        projectileFiringPeriod = baseFiringPeriod;

    }
}
