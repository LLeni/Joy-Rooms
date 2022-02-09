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

    public void SetNameScreenText(int numberScreen, string nameScreen){
        nameScreenText.text = numberScreen + ": " + nameScreen;
    }

    public void ChangeScreenFrame(int numberScreen){
        screenFramesPanel.gameObject.transform.GetChild(numberScreen).gameObject.GetComponent<Image>().sprite = screenFrameSprites[1];
    }

    public void SetMarkText(int numberScreen, char mark){
         screenFramesPanel.gameObject.transform.GetChild(numberScreen).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mark.ToString();
    }

    public void ShowResults(){

    }

}
