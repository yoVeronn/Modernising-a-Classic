using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Slider healthBar;

    public Text score;
    public Text waveNumber;

    private int health = 100;
    private int currentScore;
    private int currentWave;

    public void UpdateScore(int newScore)
    {
        currentScore += newScore;
    }

    public void UpdateWave(int newWave)
    {
        currentWave = newWave;
    }

    public void UpdateHealth(int damage)
    {
        health -= damage;
    }

    private void Update()
    {
        healthBar.value = health;

        score.text = "Score: " + currentScore;
        waveNumber.text = "Wave: " + currentWave;
    }


}
