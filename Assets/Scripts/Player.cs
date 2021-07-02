using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    private Animator animator;

    private Rigidbody playerRb;
    [SerializeField] float jumpForce = 250;
    [SerializeField] float playerSpeed = 50;
    public bool isOnGround = true;

    private GameObject enemy;
    private bool hammer = false;

    public GameObject[] bombPrefab;
    private GameObject tmpBomb;
    private bool bombLaunching;

    public GameObject gravityPrefab;
    private GameObject tmpGravity;
    private bool gravityLaunching;

    private float minPosX = -25f;
    private float maxPosX = 25f;
    private float lowerBound = -1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerRb = GetComponent<Rigidbody>();
        //enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        
        // press S for bomb
        if (Input.GetKeyDown(KeyCode.S) && bombLaunching != true)
        {
            bombLaunching = true;
            LaunchBomb();
            StartCoroutine(BombLaunchCountdownRoutine());
        }

        // press W for bomb
        else if (Input.GetKeyDown(KeyCode.W) && gravityLaunching != true)
        {
            gravityLaunching = true;
            LaunchAntiGravity();
            StartCoroutine(GravityLaunchCountdownRoutine());
        }

        // left mouse button for hammer
        Hammer();

        PlayerBounds();

        if(!animator.IsInTransition(0))
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }
    }

    void MovePlayer() // move player with arrow keys
    {

        // x-axis movement
        float horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput > 0.5)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if(horizontalInput < -0.5)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }

        // z-axis movement
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.right * playerSpeed * horizontalInput);
        playerRb.AddForce(Vector3.forward * playerSpeed * forwardInput);

        //handling move animations
        animator.SetFloat("horizontal", Mathf.Abs(horizontalInput));
        

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            animator.SetBool("isJumping", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("isJumping", false);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            
            UIManager.instance.UpdateHealth(5);  // to decrease health from health bar
        }
    }

    private void PlayerBounds()
    {
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
    
    void Hammer()
    {
        if (Input.GetMouseButtonDown(0) && hammer == false)
        {
            Debug.Log("Hammer active");
            hammer = true;

            var center = transform.position;
            float radius = 4;

            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            foreach (var hitCollider in hitColliders)
            {
                hitCollider.SendMessage("AddDamage");
                // destroy enemy
                if (hitCollider.gameObject.CompareTag("Enemy"))
                {
                    Destroy(hitCollider.gameObject);
                }
                // UI: hammer animation
                // UI: particle effect, sound effect
                // UI: AddScore per enemy
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Hammer deactivated");
            hammer = false;
        }
    }

    void LaunchBomb()  //bombDropped animator bool here
    {
        tmpBomb = Instantiate(bombPrefab[Random.Range(0, bombPrefab.Length)], transform.position + Vector3.up, Quaternion.Euler(180f,0f,0f));
        animator.SetBool("bombDropped", true);
    }

    IEnumerator BombLaunchCountdownRoutine()
    {
        yield return new WaitForSeconds(2);
        bombLaunching = false;
        animator.SetBool("bombDropped", false);
    }

    void LaunchAntiGravity()
    {
        tmpGravity = Instantiate(gravityPrefab, transform.position + Vector3.right, Quaternion.identity);
        animator.SetBool("bombDropped", true);

    }

    IEnumerator GravityLaunchCountdownRoutine()
    {
        yield return new WaitForSeconds(2);
        gravityLaunching = false;
        animator.SetBool("bombDropped", false);

    }

}
