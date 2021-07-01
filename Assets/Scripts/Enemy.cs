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
    private float lowerBound = -1;

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
        

        //destroy enemies offscreen
        if (transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    // if within slashRadius?
        //    Destroy(gameObject);
        //}
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
