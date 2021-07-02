using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private TransformPlayer transformPlayerScript;

    [SerializeField] float enemySpeed = 3;
    [SerializeField] float enemySpeedGravity = 10;
    private Rigidbody enemyRb;
    private GameObject player;
    private GameObject playerFat;
    private GameObject antiGravity;

    private float lowerBoundY = -1;
    private float minPosX = -25f;
    private float maxPosX = 25f;
    private float zBound = 5;

    private int scorePerEnemy = 5;

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
        antiGravity = GameObject.Find("AntiGravity");
        transformPlayerScript = GameObject.Find("GameManager").GetComponent<TransformPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // player attraction
        if (transformPlayerScript.PlayerActive == true)
        {
            Vector3 lookDirectionNormal = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirectionNormal * enemySpeed);
        }

        // fat player attraction
        else if (transformPlayerScript.PlayerFatActive == true)
        {
            Vector3 lookDirectionFat = (playerFat.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirectionFat * enemySpeed);
        }

        //// anti-gravity attraction
        //Vector3 lookDirectionGravity = (antiGravity.transform.position - transform.position).normalized;
        //enemyRb.AddForce(lookDirectionGravity * enemySpeedGravity);

        //destroy enemies offscreen, add to score
        if (transform.position.x < minPosX)
        {
            Destroy(gameObject);
            //UI: AddScore++
            UIManager.instance.UpdateScore(scorePerEnemy);
        }
        else if (transform.position.x > maxPosX)
        {
            Destroy(gameObject);
            //UI: AddScore++
            UIManager.instance.UpdateScore(scorePerEnemy);
        }
        if (transform.position.y < lowerBoundY)
        {
            Destroy(gameObject);
            // UI: AddScore++
            UIManager.instance.UpdateScore(scorePerEnemy);
        }

        if ((transform.position.z < -zBound) || (transform.position.z > zBound))
        {
            Destroy(gameObject);
            // UI: AddScore++
            UIManager.instance.UpdateScore(scorePerEnemy);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    // if within slashRadius?
        //    Destroy(gameObject);
        //}

        // IF ANTI-GRAVITY IS PRESENT
        // add navMesh AI
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
       // Instantiate(explosionParticle, transform.position, explosionParticke.transform.rotation);

    }
}
