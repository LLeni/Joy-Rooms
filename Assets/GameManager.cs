using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas resultsCanvas;

    void Awake(){
        Application.targetFrameRate = 300;
        instance = this;
    }

    public void EndScreen(){
        if(ScreenManager.instance.IsLastScreen()){
            
        }
    }

}

