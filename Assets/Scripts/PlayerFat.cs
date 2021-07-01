using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFat : MonoBehaviour
{
    private Rigidbody playerFatRb;
    [SerializeField] float jumpForce = 50;
    [SerializeField] float playerSpeed = 10;
    public bool isOnGround = true;
    private float powerupStrength = 15;

    private float minPosX = -25f;
    private float maxPosX = 25f;
    private float lowerBound = -1;

    // values to be tested and confirmed
    [SerializeField] float hangTime = 0.2f;
    [SerializeField] float smashSpeed = 20;
    [SerializeField] float explosionForce = 50;
    [SerializeField] float explosionRadius = 30;
    
    public bool smashing = false;
    float floorY;

    // Start is called before the first frame update
    void Start()
    {
        playerFatRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        float forwardInput = Input.GetAxis("Vertical");
        playerFatRb.AddForce(transform.forward * forwardInput * playerSpeed);

        // when we press S and smashing is not currently active,
        if (Input.GetKeyDown(KeyCode.S) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }

        // preventing player from falling out of map
        if (transform.position.x < minPosX)
        {
            transform.position = new Vector3(minPosX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxPosX)
        {
            transform.position = new Vector3(maxPosX, transform.position.y, transform.position.z);
        }

        // game over if player falls out of map
        if (transform.position.y < lowerBound)
        {
            Debug.Log("Game Over");
            // UI: game over scene activated
        }
    }

    void MovePlayer() // move player with arrow keys
    {
        // x-axis movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // z-axis movement
        //float forwardInput = Input.GetAxis("Vertical");

        playerFatRb.AddForce(Vector3.right * playerSpeed * horizontalInput);
        //playerFatRb.AddForce(Vector3.forward * playerSpeed * forwardInput);

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerFatRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator Smash() // working, but player flies far away after explosion
    {
        var enemies = FindObjectsOfType<Enemy>();

        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;

        while(Time.time < jumpTime)
        {
            playerFatRb.velocity = new Vector2(playerFatRb.velocity.x, smashSpeed);
            yield return null;
        }

        while(transform.position.y > floorY)
        {
            playerFatRb.velocity = new Vector2(playerFatRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        // cycle through all enemies
        for (int i = 0; i< enemies.Length; i++)
        {
            // Apply an exploision force originating from our position
            if(enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 1.0f, ForceMode.Impulse);
            }
        }

        smashing = false;
    }
}
