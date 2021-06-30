using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFat : MonoBehaviour
{
    private Rigidbody playerFatRb;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float playerSpeed = 10;
    public bool isOnGround = true;
    private float powerupStrength = 15;

    // Start is called before the first frame update
    void Start()
    {
        playerFatRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }


    void MovePlayer() // move player with arrow keys
    {
        // x-axis movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // z-axis movement
        float forwardInput = Input.GetAxis("Vertical");

        playerFatRb.AddForce(Vector3.right * playerSpeed * horizontalInput);
        playerFatRb.AddForce(Vector3.forward * playerSpeed * forwardInput);

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerFatRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        // add extra bouncy force
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
}
