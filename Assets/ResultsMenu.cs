using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Data;

public class ResultsMenu : MonoBehaviour
{
    // 0 - back
    // 1 - cancel
    // 2 - start
   [SerializeField] private GameObject[] buttonContainers;

    [SerializeField] private float timeForAnimation;

    private const int START_LEVEL_BUTTONS = 3;


    private int idCurrentButton;

    private float timeElapsed;

    private int numberSelectedLevel; //Из массива levels

    private int numberLastAvailableLevel;

    void Awake()
    {
        idCurrentButton = 1;
        timeElapsed = 0;
        buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);

    }

    void Update()
    {  
        timeElapsed++;

        if(Input.GetKeyDown("right") || Input.GetKeyDown("tab") || Input.GetKeyDown("left")){
            DeactivateSprites(idCurrentButton);
            switch(idCurrentButton){
                case 0:
                    idCurrentButton = 1;
                    break;
                case 1:
                    idCurrentButton = 0;
                    break;
            }
            timeElapsed = 0;
            buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
        }

        if(Input.GetKeyDown("return")){
            switch(idCurrentButton){
                case 0:
                    GameManager.instance.ResumeGame();
                    UIManager.instance.HideResultsMenu();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                case 1:
                    SceneManager.LoadScene("LevelMenu");
                    break;
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
    private void DeactivateSprites(int idButton){
        buttonContainers[idButton].transform.GetChild(0).gameObject.SetActive(false);
        buttonContainers[idButton].transform.GetChild(1).gameObject.SetActive(false);
    }
}
