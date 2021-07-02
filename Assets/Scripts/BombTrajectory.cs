using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrajectory : MonoBehaviour
{
    private Rigidbody bombRb;

    private float explosionForce = 50;
    private float explosionRadius = 30;
    
    public ParticleSystem travelEffect;
    public ParticleSystem explosionEffect;

    void Start()
    {
        bombRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        travelEffect.Play();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Ground"))
        {
            
            var enemies = FindObjectsOfType<Enemy>();
            for (int i = 0; i < enemies.Length; i++)
            {
                // Apply an exploision force originating from our position
                if (enemies[i] != null)
                {
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 1.0f, ForceMode.Impulse);
                    Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
                    Destroy(gameObject);
                }
            }
        }
    }
}
