using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{ 
    public GameObject[] enemyVerticalPrefabs;
    public GameObject[] enemyHorizontalPrefabs;
    public GameObject finalBoss;
    private float startDelay = 1;
    private float minRepeatRate = 1;
    private float maxRepeatRate = 2.5f;

    public bool waveActive = true;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        StartCoroutine(waveTimer());

    }

    // Update is called once per frame
    void Update()
    {


        if (waveActive == false) // prev wave ended
        {

            if (waveNumber < 5)
            { 
                UIManager.instance.UpdateWave(waveNumber);

                // UI: change background  //maybe change background lighting
                SpawnEnemyWave(waveNumber);
                StartCoroutine(waveTimer());

                waveNumber++;
            }

            else if (waveNumber >= 5)
            {
                waveNumber = 5;
                StopCoroutine(waveTimer());
                Debug.Log("Final boss");
                // UI: change background
                SpawnFinalBoss();
            }
        }
    }

    IEnumerator waveTimer()
    {
        yield return new WaitForSeconds(15);
        waveActive = false;
    }

    void SpawnEnemyWave(int waveNumber)
    {
        waveActive = true;
        InvokeRepeating("SpawnVerticalEnemy", startDelay, Random.Range(minRepeatRate, maxRepeatRate));
        InvokeRepeating("SpawnHorizontalEnemy", startDelay, Random.Range(minRepeatRate, maxRepeatRate));
    }

    void SpawnVerticalEnemy()
    {
        int verticalIndex = Random.Range(0, waveNumber);
        Instantiate(enemyVerticalPrefabs[verticalIndex], new Vector3(Random.Range(-20, 20), Random.Range(1, 10), 0), enemyVerticalPrefabs[verticalIndex].transform.rotation);
    }

    void SpawnHorizontalEnemy()
    {
        int horizontalIndex = Random.Range(0, waveNumber);
        Instantiate(enemyHorizontalPrefabs[horizontalIndex], new Vector3(Random.Range(-20, 20), Random.Range(1, 5), 0), enemyHorizontalPrefabs[horizontalIndex].transform.rotation);
    }

    void SpawnFinalBoss()
    {
        waveActive = true;
        Instantiate(finalBoss, new Vector3(20, Random.Range(1, 5), 0), finalBoss.transform.rotation);
    }
}
