using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public GameObject[] enemyVerticalPrefabs;
    public GameObject[] enemyHorizontalPrefabs;
    private float startDelay = 2;
    private float repeatRate = 4;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnVerticalEnemy", startDelay, repeatRate);
        InvokeRepeating("SpawnHorizontalEnemy", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnVerticalEnemy()
    {
        int verticalIndex = Random.Range(0, enemyVerticalPrefabs.Length);
        Instantiate(enemyVerticalPrefabs[verticalIndex], new Vector3(Random.Range(-20, 20), Random.Range(1, 10), 0), enemyVerticalPrefabs[verticalIndex].transform.rotation);
    }

    void SpawnHorizontalEnemy()
    {
        int horizontalIndex = Random.Range(0, enemyHorizontalPrefabs.Length);
        Instantiate(enemyHorizontalPrefabs[horizontalIndex], new Vector3(Random.Range(-20, 20), Random.Range(1, 5), 0), enemyHorizontalPrefabs[horizontalIndex].transform.rotation);
    }
}
