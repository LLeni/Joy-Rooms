using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class NewGameMenu : MonoBehaviour
{

   [SerializeField] private GameObject[] buttonContainers;
    [SerializeField] private float timeForAnimation;
    private bool isActive;
    private int idCurrentButton = 0;

    private float timeElapsed;

    private Profile[] profiles;

    void Awake()
    {
        isActive = false;
        idCurrentButton = 0;
        timeElapsed = 0;
        buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
    
    }



    void Update()
    {  
        timeElapsed++;

        if(idCurrentButton == 0){
            if(Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("tab")){
                DeactivateSprites(idCurrentButton);
                idCurrentButton = 1;
                timeElapsed = 0;
                buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
            }

            if(Input.GetKeyDown("right")){
                DeactivateSprites(idCurrentButton);
                idCurrentButton = 2;
                timeElapsed = 0;
                buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        if(idCurrentButton == 1){
            if(Input.GetKeyDown("up")){
                DeactivateSprites(idCurrentButton);
                idCurrentButton = 0;
                timeElapsed = 0;
                buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
            }

            if(Input.GetKeyDown("right") || Input.GetKeyDown("tab")){
                DeactivateSprites(idCurrentButton);
                idCurrentButton = 2;
                timeElapsed = 0;
                buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        
        if(idCurrentButton == 2){
            if(Input.GetKeyDown("up") || Input.GetKeyDown("tab")){
                DeactivateSprites(idCurrentButton);
                idCurrentButton = 0;
                timeElapsed = 0;
                buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
            }

            if(Input.GetKeyDown("left")){
                DeactivateSprites(idCurrentButton);
                idCurrentButton = 1;
                timeElapsed = 0;
                buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        if(Input.GetKeyDown("return")){
            switch(idCurrentButton){
                case 0:
                    buttonContainers[idCurrentButton].transform.GetChild(2).gameObject.GetComponent<TMP_InputField>().Select();
                    buttonContainers[idCurrentButton].transform.GetChild(2).gameObject.GetComponent<TMP_InputField>().ActivateInputField();
                    break;
                case 1:
                    UIMainMenuManager.instance.HideNewGamePanel();
                    break;
                case 2:
                    TMP_InputField inputField = buttonContainers[0].transform.GetChild(2).gameObject.GetComponent<TMP_InputField>();
                    
  
                    if(inputField.text != null || inputField.text != ""){

                        DBConnector.ExecuteQueryWithoutAnswer($"INSERT INTO profile(login_profile, date_registration_profile, is_fake_profile) VALUES ('{inputField.text}', '{DateTime.Now.ToString("dd.MM.yyyy")}','false');");
                        
            
                        Session.currentProfile = new Profile(int.Parse(DBConnector.GetTable("SELECT MAX(profile.id_profile) FROM profile;").Rows[0][0].ToString()), inputField.text);
                    
                        Debug.Log("login = " +  Session.currentProfile.GetLoginProfile());
                    
                        SceneManager.LoadScene("LevelMenu");
                    } else {
                        inputField.text = "Error! No Profile Name!";
                    }

                    break;

            }


        }
        if(Input.GetKeyDown("escape")){
            UIMainMenuManager.instance.HideNewGamePanel();
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
