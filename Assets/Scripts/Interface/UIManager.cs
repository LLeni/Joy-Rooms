using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas resultsCanvas;
    [SerializeField] private Canvas HUDCanvas;

    [SerializeField] private TextMeshProUGUI amountDeathText;

    
    [SerializeField] private TextMeshProUGUI levelTimeText;
    [SerializeField] private TextMeshProUGUI screenTimeText;
    [SerializeField] private TextMeshProUGUI nameScreenText;

    [SerializeField] private GameObject screenFramesPanel;
    [SerializeField] private Sprite[] screenFrameSprites;

    [SerializeField] private GameObject abilityPanel;

    [SerializeField] private Sprite blankAbilitySprite;

    public static UIManager instance;

    void Start()
    {
        ChangeScreenFrame(0);
        instance = this;
    }
    void Update()
    {
        levelTimeText.text =  "Level Time:          " + Stopwatch.instance.GetLevelTime().Substring(3);
        screenTimeText.text =  "Room Time:         " + Stopwatch.instance.GetScreenTime().Substring(3);
    }

    public void ShowPauseMenu(){
        pauseCanvas.gameObject.SetActive(true);
        HUDCanvas.gameObject.SetActive(false);
    }

    public void HidePauseMenu(){
        pauseCanvas.gameObject.SetActive(false);
        HUDCanvas.gameObject.SetActive(true);
    }

    public void SetAmountDeathText(int amountDeath){
       amountDeathText.text =  "x   " + amountDeath;
    }

    public void SetNameScreenText(string nameScreen){
        nameScreenText.text = nameScreen;
    }

    public void ChangeScreenFrame(int numberScreen){
        screenFramesPanel.gameObject.transform.GetChild(numberScreen).gameObject.GetComponent<Image>().sprite = screenFrameSprites[1];
    }

    public void SetMarkText(int numberScreen, char mark){
         screenFramesPanel.gameObject.transform.GetChild(numberScreen).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mark.ToString();
    }

    public void ShowResultsMenu(){
        SetUpResultsContent();
        resultsCanvas.gameObject.SetActive(true);
    }

    public void HideResultsMenu(){
        resultsCanvas.gameObject.SetActive(false);
    }

    public void SetUpResultsContent(){
        GameObject resultsPanel = resultsCanvas.gameObject.transform.GetChild(0).gameObject;
        GameObject levelInformationPanel = resultsPanel.transform.GetChild(1).gameObject;
        GameObject timePanel = levelInformationPanel.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        GameObject deathPanel = levelInformationPanel.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;

        Level level = GameManager.instance.GetLevel();

        int idLevel = SectionManager.instance.GetIdSection(level.levelName);
        int[] idScreens = new int[level.screensNames.Length];
        for(int i = 0; i < idScreens.Length; i++){
            idScreens[i] = SectionManager.instance.GetIdSection(level.screensNames[i]);
        }

        //Title
        resultsPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = level.levelName;
    

        //Mark Level
        char mark = RecordsManager.instance.GetMarkRecord(SectionManager.instance.GetIdSection(level.levelName), ResultsManager.instance.GetResultLevel().GetTimeRunRecord(), ResultsManager.instance.GetResultLevel().GetAmountDeath());

        levelInformationPanel.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = mark.ToString();

        //Times Level
        timePanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Time  Need for J:   " + RecordsManager.instance.GetSpecificEvaluation(idLevel, 'J').Substring(3);

        Record levelResult = ResultsManager.instance.GetResultLevel();
        timePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Your record:                 " + levelResult.GetTimeRunRecord().Substring(3) + (ResultsManager.instance.IsNewRecordLevel() ? " [New]" : "");
        
        //Amount Death Level
        deathPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "x " + levelResult.GetAmountDeath();

        //Screens
        GameObject screenPanel;
        GameObject screenFrame;
        GameObject screenTimePanel;
        Record screenResult;
        for(int i = 0; i < idScreens.Length; i++){
            screenPanel = resultsPanel.transform.GetChild(3 + i).gameObject;
            screenFrame = screenPanel.transform.GetChild(0).gameObject;
            screenTimePanel = screenPanel.transform.GetChild(1).gameObject;

            screenResult = ResultsManager.instance.GetResultScreen(i);
            //Mark Screen
            screenFrame.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = RecordsManager.instance.GetMarkRecord(idScreens[i], screenResult.GetTimeRunRecord(), screenResult.GetAmountDeath()).ToString();

            //Title Screen
            screenFrame.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = level.screensNames[i];
        
            //Times Screen
            screenTimePanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Time  Need for S:   " + RecordsManager.instance.GetSpecificEvaluation(idScreens[i], 'S').Substring(3);
            screenTimePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Your record:                 " + screenResult.GetTimeRunRecord().Substring(3) + (ResultsManager.instance.IsNewRecordScreen(i) ? " [New]" : "");
        }
    }

    public void SetAbilityImage(Sprite abilitySprite){
        abilityPanel.GetComponent<Image>().sprite = abilitySprite;
    }

    public void RefreshAbilityImage(){
        abilityPanel.GetComponent<Image>().sprite = blankAbilitySprite;
    }

}
