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

    //public static bool GetMouseButtonDown(int button);
    //private GameObject enemy;
    //private bool slashLeft, slashRight, slashUp, slashDown;
    //private Vector2 startPosMouse, mouseDelta;
    //private bool slashing = false;
    //private float slashRadius = 5.0f;

    public GameObject bombPrefab;
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
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.right * playerSpeed * horizontalInput);
        playerRb.AddForce(Vector3.forward * playerSpeed * forwardInput);

        //handling move animations
        

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

    //void Slash()
    //{
    //    // mouse slashing to destroy enemies
    //    slashLeft = slashRight = slashUp = slashDown = false;

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Debug.Log("Slash active");
    //        slashing = true;
    //        startPosMouse = Input.mousePosition;
    //    }
    //    else if (Input.GetMouseButtonUp(0))
    //    {
    //        Debug.Log("Slash deactivated");
    //        slashing = false;
    //        Reset();
    //    }

    //    // check directions of mouse slash
    //    mouseDelta = Vector2.zero;
    //    if (slashing == true)
    //    {
    //        mouseDelta = new Vector2((Input.mousePosition.x - startPosMouse.x), (Input.mousePosition.y - startPosMouse.y));

    //        float slashX = mouseDelta.x;
    //        float slashY = mouseDelta.y;

    //        if (slashX < 0)
    //        {
    //            slashLeft = true;
    //            Debug.Log("Slash Left");
    //        }

    //        if (slashX > 0)
    //        {
    //            slashRight = true;
    //        }

    //        if (slashY < 0)
    //        {
    //            slashDown = true;
    //        }

    //        if (slashY > 0)
    //        {
    //            slashUp = true;
    //        }

    //        Reset();
    //    }

    //    // find enemies within slash radius and destroy
    //    if (slashLeft)
    //    {
    //        if ((transform.position.x - enemy.transform.position.x) < slashRadius)
    //        {
    //            Destroy(enemy);
    //            Debug.Log("Left Attack");
    //        }
    //    }
    //    if (slashRight)
    //    {
    //        if ((enemy.transform.position.x - transform.position.x) < slashRadius)
    //        {
    //            Destroy(enemy);
    //            Debug.Log("Right Attack");
    //        }
    //    }
    //    if (slashDown)
    //    {
    //        if ((transform.position.y - enemy.transform.position.y) < slashRadius)
    //        {
    //            Destroy(enemy);
    //        }
    //    }
    //    if (slashUp)
    //    {
    //        if ((enemy.transform.position.y - transform.position.y) < slashRadius)
    //        {
    //            Destroy(enemy);
    //        }
    //    }
    //}
    //private void Reset()
    //{
    //    startPosMouse = mouseDelta = Vector2.zero;
    //    slashing = false;
    //}

    void LaunchBomb()
    {
        tmpBomb = Instantiate(bombPrefab, transform.position + Vector3.up, Quaternion.identity);
    }

    IEnumerator BombLaunchCountdownRoutine()
    {
        yield return new WaitForSeconds(2);
        bombLaunching = false;
    }

    void LaunchAntiGravity()
    {
        tmpGravity = Instantiate(gravityPrefab, transform.position + Vector3.right, Quaternion.identity);
    }

    IEnumerator GravityLaunchCountdownRoutine()
    {
        yield return new WaitForSeconds(2);
        gravityLaunching = false;
    }

}
