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

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    

    void Update()
    {
        currentLevelTime += Time.deltaTime;
        currentScreenTime += Time.deltaTime;
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
