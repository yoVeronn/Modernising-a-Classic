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
    }
}
