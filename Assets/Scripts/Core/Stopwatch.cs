using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelTimeText;
    [SerializeField] private TextMeshProUGUI screenTimeText;

    private float currentLevelTime;
    private float currentScreenTime;

    public Stopwatch instance;

    void Start()
    {
        instance = this;

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    

    void Update()
    {
        currentLevelTime += Time.deltaTime;
        currentScreenTime += Time.deltaTime;

        TimeSpan levelTime = TimeSpan.FromSeconds(currentLevelTime);
        TimeSpan screenTime  = TimeSpan.FromSeconds(currentScreenTime);
        levelTimeText.text =  "Level Time:          " + (levelTime.Minutes < 10 ? "0": "") + levelTime.Minutes.ToString()  + ":" + (levelTime.Seconds < 10 ? "0": "") + levelTime.Seconds.ToString() + "." + levelTime.Milliseconds.ToString();
        screenTimeText.text =  "Room Time:         " + (screenTime.Minutes < 10 ? "0": "") + screenTime.Minutes.ToString() + ":" + (screenTime.Seconds < 10 ? "0": "") + screenTime.Seconds.ToString() + "." + screenTime.Milliseconds.ToString();
    }

    public void ResetLevelTime(){
        currentLevelTime = 0;
    }

    public void ResetScreenTime(){
        currentScreenTime = 0;
    }

    public float GetLevelTime(){
        return currentLevelTime;
    }

    public float GetScreenTime(){
        return currentScreenTime;
    }

    private void OnGameStateChanged(GameState newGameState){
        enabled = newGameState == GameState.Gameplay;
    }
}
