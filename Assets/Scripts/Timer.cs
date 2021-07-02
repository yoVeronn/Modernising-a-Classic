using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private Text timer;
    private float currentTime;


    private void Awake()
    {
        timer = GetComponent<Text>();
        UIManager.OnGameOver += resetTime;
    }

    private void Update()
    {
        currentTime = Time.time;
        timer.text = "" + Mathf.RoundToInt(currentTime);
    }

    public void resetTime()
    {
        currentTime = 0f;
    }
}
