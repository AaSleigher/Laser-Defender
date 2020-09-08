using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;
    [SerializeField] GameObject powerup = null;
    [SerializeField] float powerupSpeed;

    [Header ("Shoot Components")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration = 0.2f;

    [Header ("Audio")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)]float deathSoundVolume = .7f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = .5f;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        DropPowerUp(powerup);
        GameObject explosion = Instantiate(
            deathVFX,
            transform.position,
            transform.rotation);
        Destroy(explosion, explosionDuration);

        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    private void DropPowerUp(GameObject powerup)
    {
        if (powerup == null)
        {
            return;
        }
        powerup = Instantiate(
           powerup,
           transform.position,
           Quaternion.identity) as GameObject;
        powerup.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -powerupSpeed);
    }
}
