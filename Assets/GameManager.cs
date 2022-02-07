using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    private int amountDeath;

    void Awake(){
        Application.targetFrameRate = 300;
        amountDeath = 0;
        instance = this;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            GameState currentGameState = GameStateManager.Instance.CurrentGameState;
            if(currentGameState == GameState.Gameplay){
                GameStateManager.Instance.SetState(GameState.Paused);
                UIManager.instance.ShowPauseMenu();
            } else {
                GameStateManager.Instance.SetState(GameState.Gameplay);
                UIManager.instance.HidePauseMenu();
            }
        }   
    }

    public void EndScreen(){
        if(ScreenManager.instance.IsLastScreen()){
            
        }
        UIManager.instance.SetMarkText(ScreenManager.instance.GetNumberScreen(), "S");
        ScreenManager.instance.NextScreen();
        Stopwatch.instance.ResetScreenTime();
        UIManager.instance.ChangeScreenFrame(ScreenManager.instance.GetNumberScreen());
    }

    public void RestartScreen(){
        ScreenManager.instance.RestartScreen();
        Stopwatch.instance.ResetScreenTime();
        UIManager.instance.SetAmountDeathText(++amountDeath);
    }


}

