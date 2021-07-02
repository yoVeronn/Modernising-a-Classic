using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    public Slider healthBar;

    public Text score;
    public Text waveNumber;

    public Text gameoverScore;
    public Text gameoverWave;

    public Button restartButton;
    public Button menuButton;

    public GameObject scorePanel;
    public GameObject gameOverPanel;

    private int health = 100;
    private int currentScore;
    private int currentWave;

    public delegate void onGameOver();
    public static event onGameOver OnGameOver;

    private void Start()
    {
        scorePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

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

    public void GameOverScreen()
    {
        scorePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        gameoverScore.text = "Score: " + currentScore;
        gameoverWave.text = "Wave: " + currentWave;

        Time.timeScale = 0f;
        
    }

    public void RestartLevel()
    {
        if(Time.timeScale != 1.0f)
        { 
            Time.timeScale = 1.0f;
        }

        SceneManager.LoadScene(1);
        OnGameOver.Invoke();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        healthBar.value = health;

        score.text = "Score: " + currentScore;
        waveNumber.text = "Wave: " + currentWave;

        if(health == 0)
        {
            GameOverScreen();
        }
    }

}
