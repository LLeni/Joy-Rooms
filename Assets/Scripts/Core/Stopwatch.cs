using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{

    private float currentLevelTime;
    private float currentScreenTime;

    public static Stopwatch instance;

    void Start()
    {
        instance = this;

       // GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    

    void Update()
    {
        currentLevelTime += Time.deltaTime;
        currentScreenTime += Time.deltaTime;

        //Debug.Log(new DateTime(0, 0, 0, 0, 0, 0, TimeSpan.FromSeconds(currentLevelTime).Milliseconds).ToString("hh:mm:ss.fff"));
    }

    public string GetLevelTime(){
        TimeSpan levelTime = TimeSpan.FromSeconds(currentLevelTime);

        return (levelTime.Hours < 10 ? "0": "") + levelTime.Hours.ToString() + ":" + (levelTime.Minutes < 10 ? "0": "") + levelTime.Minutes.ToString()  + ":" + (levelTime.Seconds < 10 ? "0": "") + levelTime.Seconds.ToString() + "." + levelTime.Milliseconds.ToString();
        
    }

    public string GetScreenTime(){
        TimeSpan screenTime  = TimeSpan.FromSeconds(currentScreenTime);
        return (screenTime.Hours < 10 ? "0": "") + screenTime.Hours.ToString() + ":" + (screenTime.Minutes < 10 ? "0": "") + screenTime.Minutes.ToString() + ":" + (screenTime.Seconds < 10 ? "0": "") + screenTime.Seconds.ToString() + "." + screenTime.Milliseconds.ToString();
    
    }

    public void ResetLevelTime(){
        currentLevelTime = 0;
    }

    public void ResetScreenTime(){
        currentScreenTime = 0;
    }


    private void OnGameStateChanged(GameState newGameState){
        enabled = newGameState == GameState.Gameplay;
    }
}
