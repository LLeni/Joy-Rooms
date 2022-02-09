using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Data;

public class LevelsMenu : MonoBehaviour
{
    // 0 - back
    // 1 - cancel
    // 2 - start
   [SerializeField] private GameObject[] buttonContainers;

   [SerializeField] private Level[] levels;
    [SerializeField] private float timeForAnimation;

    private const int START_LEVEL_BUTTONS = 3;
    private bool isActive;

    private int idCurrentButton;

    private float timeElapsed;


    private int numberSelectedLevel; //Из массива levels

    private int numberLastAvailableLevel;

    void Awake()
    {
        //TODO: Временно то, что снизу - убрать после
        //Session.currentProfile = new Profile(0, "itLLeni");

        isActive = false;
        idCurrentButton = START_LEVEL_BUTTONS;
        timeElapsed = 0;
        buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);

        SetUpLevelButtons();
    }

    private void SetUpLevelButtons(){

        string nameLastFinishedLevel = RecordsManager.instance.GetNameLastFinishedLevel(Session.currentProfile.GetIdProfile());
        if(nameLastFinishedLevel == null){

            buttonContainers[START_LEVEL_BUTTONS].transform.GetChild(3).gameObject.SetActive(true);
            buttonContainers[START_LEVEL_BUTTONS].transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "-";
        } else {
            int numberLastFinishedLevel = START_LEVEL_BUTTONS;
            for(int i = 0; i < levels.Length; i++){
                if(levels[i].levelName == nameLastFinishedLevel){
                    numberLastFinishedLevel = i + START_LEVEL_BUTTONS;
                }
            }
            numberLastAvailableLevel = numberLastFinishedLevel + 1;
            
            int idCurrentLevel;
            Record record;
            for(int i = START_LEVEL_BUTTONS; i < buttonContainers.Length; i++){

                idCurrentLevel = SectionManager.instance.GetIdSection(levels[i - START_LEVEL_BUTTONS].levelName);
                record = RecordsManager.instance.GetRecord(Session.currentProfile.GetIdProfile(), idCurrentLevel);
                if(record == null){
                    buttonContainers[i].transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "-";
                } else {
                    buttonContainers[i].transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = RecordsManager.instance.GetMarkRecord(idCurrentLevel, record.GetTimeRunRecord(), record.GetAmountDeath()).ToString();
                }
                buttonContainers[i].transform.GetChild(3).gameObject.SetActive(true);
                if(i == numberLastAvailableLevel){
                    break;
                }
            }

        }
    }


    void Update()
    {  
        timeElapsed++;

        if(Input.GetKeyDown("right") || Input.GetKeyDown("tab")){
            DeactivateSprites(idCurrentButton);

            if(idCurrentButton == 1){
                idCurrentButton = 2;
            } else if(idCurrentButton == 2) {
                idCurrentButton = 1;
            } else if(idCurrentButton == 0){
                idCurrentButton = START_LEVEL_BUTTONS;
            } else if(idCurrentButton == numberLastAvailableLevel){
                idCurrentButton = 0;
            } else if(idCurrentButton + 1 != buttonContainers.Length){

                if(IsLevelAvailable(idCurrentButton + 1)){
                    idCurrentButton++;
                }
            }
            timeElapsed = 0;
            buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
        }

        
        if(Input.GetKeyDown("left")){
            DeactivateSprites(idCurrentButton);
            switch(idCurrentButton){
                case 0:
                    idCurrentButton = numberLastAvailableLevel;
                    break;
                case 1:
                    idCurrentButton = 2;
                    break;
                case 2:
                    idCurrentButton = 1;
                    break;
                case START_LEVEL_BUTTONS:
                    idCurrentButton = 0;
                    break;
                default:
                    idCurrentButton--;
                    break;
            }
            
            timeElapsed = 0;
            buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
        }

        if(Input.GetKeyDown("return")){

            switch(idCurrentButton){
                case 0:
                    SceneManager.LoadScene("MainMenu");
                    break;
                case 1:
                    DeactivateSprites(idCurrentButton);
                    idCurrentButton = numberSelectedLevel + START_LEVEL_BUTTONS;
                    buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    break;
                case 2:
                    SceneManager.LoadScene(levels[numberSelectedLevel].name);
                    break;
                default:
                    numberSelectedLevel = idCurrentButton - START_LEVEL_BUTTONS;
                    
                    SetUpDescription();
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    DeactivateSprites(idCurrentButton);
                    idCurrentButton = 1;
                    buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
                    break;
            }
        }

        if(Input.GetKeyDown("escape")){
            if(idCurrentButton == 1 ||idCurrentButton == 2){
                DeactivateSprites(idCurrentButton);
                idCurrentButton = numberSelectedLevel + START_LEVEL_BUTTONS;
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            } else {
                SceneManager.LoadScene("MainMenu");
            }
        }
        
        if(timeElapsed >= timeForAnimation){
            timeElapsed = 0;
            if(buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.activeSelf){
                buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(false);
                buttonContainers[idCurrentButton].transform.GetChild(1).gameObject.SetActive(true);
            } else {
                buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
                buttonContainers[idCurrentButton].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    private void SetUpDescription(){
        GameObject descriptionPanel = this.gameObject.transform.GetChild(0).gameObject;
        GameObject levelInformationPanel = descriptionPanel.transform.GetChild(1).gameObject;
        GameObject timePanel = levelInformationPanel.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        GameObject deathPanel = levelInformationPanel.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;

        int idLevel = SectionManager.instance.GetIdSection(levels[numberSelectedLevel].levelName);
        int[] idScreens = new int[levels[numberSelectedLevel].screensNames.Length];
        for(int i = 0; i < idScreens.Length; i++){
            idScreens[i] = SectionManager.instance.GetIdSection(levels[numberSelectedLevel].screensNames[i]);
        }

        //Title
        descriptionPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levels[numberSelectedLevel].levelName;
    

        //Mark Level
        char mark = char.Parse(buttonContainers[numberSelectedLevel + START_LEVEL_BUTTONS].transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text);
        bool isHaveRecord = mark == '-' ? false : true;
  
  
        levelInformationPanel.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = mark.ToString();

        //Times Level
        timePanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Time  Need for J: " + RecordsManager.instance.GetSpecificEvaluation(idLevel, 'J').Substring(3);

        if(isHaveRecord){
            Record levelRecord = RecordsManager.instance.GetRecord(Session.currentProfile.GetIdProfile(), idLevel);
            timePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Your record:               " + levelRecord.GetTimeRunRecord().Substring(3);;
            
            //Amount Death Level
            deathPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "x " + levelRecord.GetAmountDeath();
        }

        //Screens
        GameObject screenPanel;
        GameObject screenFrame;
        GameObject screenTimePanel;
        GameObject screenDeathPanel;
        Record recordScreen;
        for(int i = 0; i < idScreens.Length; i++){
            screenPanel = descriptionPanel.transform.GetChild(3 + i).gameObject;
            screenFrame = screenPanel.transform.GetChild(0).gameObject;
            screenTimePanel = screenPanel.transform.GetChild(1).gameObject;
            screenDeathPanel = screenPanel.transform.GetChild(2).gameObject;
            Debug.Log(screenTimePanel + " " + screenDeathPanel);
            recordScreen = RecordsManager.instance.GetRecord(Session.currentProfile.GetIdProfile(), idScreens[i]);
            
            //Mark Screen
            if(recordScreen == null){
                screenFrame.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "-";
            } else {
                screenFrame.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = RecordsManager.instance.GetMarkRecord(idScreens[i], recordScreen.GetTimeRunRecord(), recordScreen.GetAmountDeath()).ToString();
            }

            //Title Screen
            screenFrame.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = levels[numberSelectedLevel].screensNames[i];
        
            //Times Screen
            screenTimePanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Time  Need for S: " + RecordsManager.instance.GetSpecificEvaluation(idScreens[i], 'S').Substring(3);
            if(recordScreen != null)
                screenTimePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Your record:               " + recordScreen.GetTimeRunRecord().Substring(3);;
            
            //Amount Death Screen
            if(recordScreen != null)
                screenDeathPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "x " + recordScreen.GetAmountDeath();
        }
    }

    private bool isDescriptionShowed(){
        if(this.gameObject.transform.GetChild(0).gameObject.activeSelf){
            return true;
        } else {
            return false;
        }
    }

    private bool IsLevelAvailable(int numberLevel){
        if(buttonContainers[numberLevel].transform.GetChild(3).gameObject.activeSelf){
            return true;
        } else {
            return false;
        }
    }

    private void DeactivateSprites(int idButton){
        buttonContainers[idButton].transform.GetChild(0).gameObject.SetActive(false);
        buttonContainers[idButton].transform.GetChild(1).gameObject.SetActive(false);
    }
}
