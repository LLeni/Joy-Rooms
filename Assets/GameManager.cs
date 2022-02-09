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


        //TODO
        //Временные объявления
        //Они должны быть первый при выборе профиля, второй при выборе уровня
        Session.currentProfile = new Profile(0, "itLLeni");
        Session.currentLevel = new Section(1, "After Conveyor Bell", new Dictionary<char, float>());
    }

    void Update(){

       // Debug.Log(ScreenRecordsManager.instance.GetMarkRecord(1, "00:01:05"));
        if(Input.GetKeyDown(KeyCode.Escape)){
            GameState currentGameState = GameStateManager.Instance.CurrentGameState;
            if(currentGameState == GameState.Gameplay){
                GameStateManager.Instance.SetState(GameState.Paused);
                Time.timeScale = 0;
                UIManager.instance.ShowPauseMenu();
            } else {
                GameStateManager.Instance.SetState(GameState.Gameplay);
                Time.timeScale = 1;
                UIManager.instance.HidePauseMenu();
            }
        }   
    }

    public void EndScreen(){
        if(ScreenManager.instance.IsLastScreen()){
            UIManager.instance.ShowResults();
        }
        UIManager.instance.SetMarkText(ScreenManager.instance.GetNumberScreen(), ScreenRecordsManager.instance.GetMarkRecord(ScreenManager.instance.GetNumberScreen(), Stopwatch.instance.GetScreenTime()));
        ScreenManager.instance.NextScreen();
        Stopwatch.instance.ResetScreenTime();
        UIManager.instance.ChangeScreenFrame(ScreenManager.instance.GetNumberScreen());
    }

    public void RestartScreen(){
        ScreenManager.instance.RestartScreen();
        Stopwatch.instance.ResetScreenTime();
        UIManager.instance.SetAmountDeathText(++amountDeath);
    }

    public void PauseGame(){
        GameStateManager.Instance.SetState(GameState.Paused);
        Time.timeScale = 0;
    }

    public void ResumeGame(){
        GameStateManager.Instance.SetState(GameState.Gameplay);
        Time.timeScale = 1;
    }


}

