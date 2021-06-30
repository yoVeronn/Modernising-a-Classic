using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float playerSpeed = 10;
    public bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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

        playerRb.AddForce(Vector3.right * playerSpeed * horizontalInput);
        playerRb.AddForce(Vector3.forward * playerSpeed * forwardInput);

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
