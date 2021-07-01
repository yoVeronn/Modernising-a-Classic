using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private TransformPlayer transformPlayerScript;

    [SerializeField] float enemySpeed = 3;
    private Rigidbody enemyRb;
    private GameObject player;
    private GameObject playerFat;
    private float lowerBoundY = -1;
    private float minPosX = -25f;
    private float maxPosX = 25f;
    private float zBound = 5;

    //private GameObject enemy;
    //private bool slashLeft, slashRight, slashUp, slashDown;
    //private Vector2 startPosMouse, mouseDelta;
    //private bool slashing = false;
    //private float slashRadius = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerFat = GameObject.Find("PlayerFat");
        transformPlayerScript = GameObject.Find("GameManager").GetComponent<TransformPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transformPlayerScript.PlayerActive == true)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * enemySpeed);
        }

        else if (transformPlayerScript.PlayerFatActive == true)
        {
            Vector3 lookDirection = (playerFat.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * enemySpeed);
        }

        //destroy enemies offscreen, add to score
        if (transform.position.x < minPosX)
        {
            Destroy(gameObject);
            //UI: AddScore++
        }
        else if (transform.position.x > maxPosX)
        {
            Destroy(gameObject);
            //UI: AddScore++
        }
        if (transform.position.y < lowerBoundY)
        {
            Destroy(gameObject);
            // UI: AddScore++
        }

        if ((transform.position.z < -zBound) || (transform.position.z > zBound))
        {
            Destroy(gameObject);
            // UI: AddScore++
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    // if within slashRadius?
        //    Destroy(gameObject);
        //}

        // IF ANTI-GRAVITY IS PRESENT
        // add navMesh AI
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
       // Instantiate(explosionParticle, transform.position, explosionParticke.transform.rotation);

    }
}
