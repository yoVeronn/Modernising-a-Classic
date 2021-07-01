using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrajectory : MonoBehaviour
{
    private Rigidbody bombRb;
    private Transform target;
    //private float speed = 15.0f;
    private bool homing;

    private float explosionForce = 50;
    private float explosionRadius = 30;

    // Update is called once per frame
    void Start()
    {
        bombRb = GetComponent<Rigidbody>();
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
                    Destroy(gameObject);
                }
            }
        }
    }
}
