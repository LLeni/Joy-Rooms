using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private bool isTesting;
    public GameObject playerPrefab;

    private GameObject currentPlayer;
    public static GameManager instance;

    private int amountDeath;

    void Start(){
        Application.targetFrameRate = 300;
        amountDeath = 0;
        GameStateManager.Instance.SetState(GameState.Gameplay);

        instance = this;


        if(isTesting)
            Session.currentProfile = new Profile(0, "itLLeni");

        SpawnPlayer();
    }

    void Update(){

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
        ResultsManager.instance.SaveResultScreen(ScreenManager.instance.GetNumberScreen(),SectionManager.instance.GetIdSection(level.screensNames[ScreenManager.instance.GetNumberScreen()]), Stopwatch.instance.GetScreenTime());
        UIManager.instance.SetMarkText(ScreenManager.instance.GetNumberScreen(), ScreenRecordsManager.instance.GetMarkRecord(ScreenManager.instance.GetNumberScreen(), Stopwatch.instance.GetScreenTime()));
        if(ScreenManager.instance.IsLastScreen()){
            ResultsManager.instance.SaveResultLevel(SectionManager.instance.GetIdSection(level.levelName), Stopwatch.instance.GetLevelTime(), amountDeath);
            ResultsManager.instance.UploadResults();
            PauseGame();
            UIManager.instance.ShowResultsMenu();
        } else{
            ScreenManager.instance.NextScreen();
            UIManager.instance.SetMarkText(ScreenManager.instance.GetNumberScreen(), '-');
            Stopwatch.instance.ResetScreenTime();
            UIManager.instance.ChangeScreenFrame(ScreenManager.instance.GetNumberScreen());
            UIManager.instance.SetNameScreenText((ScreenManager.instance.GetNumberScreen() + 1) + "/" + level.screensNames.Length + ": " +level.screensNames[ScreenManager.instance.GetNumberScreen()]);
            
            //currentPlayer.
            UIManager.instance.RefreshAbilityImage();
        }
    }

    public void RestartScreen(){
        SpawnPlayer();
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

    public Level GetLevel(){
        return level;
    }

    public void SpawnPlayer(){
        currentPlayer = Instantiate(playerPrefab, ScreenManager.instance.GetPosCurrentRespawn(), Quaternion.identity);
    }

}

