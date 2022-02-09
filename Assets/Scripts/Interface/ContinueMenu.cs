using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Data;
public class ContinueMenu : MonoBehaviour
{

   [SerializeField] private GameObject[] buttonContainers;
    [SerializeField] private float timeForAnimation;
    private bool isActive;
    private int idCurrentButton = 0;

    private float timeElapsed;

    private Profile[] profiles;

    private int idCurrentShowedProfile;

    void Awake()
    {
        isActive = false;
        idCurrentButton = 0;
        timeElapsed = 0;
        buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
    }

    void OnEnable()
    {
        Debug.Log("Hello");
        GetProfiles();

        if(profiles.Length == 0){
            DeactivateArrows();
            idCurrentShowedProfile = -1;
        } else if(profiles.Length == 1){
            DeactivateArrows();
            idCurrentShowedProfile = 0;
        } else {
            idCurrentShowedProfile = 0;
            buttonContainers[0].transform.GetChild(2).gameObject.SetActive(false);
            buttonContainers[0].transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = profiles[idCurrentShowedProfile].GetLoginProfile();
        }

        Debug.Log(profiles.Length);
    }

    private void DeactivateArrows(){
        buttonContainers[0].transform.GetChild(2).gameObject.SetActive(false);
        buttonContainers[0].transform.GetChild(3).gameObject.SetActive(false);
    }

    private void NextProfile(){
        if(idCurrentShowedProfile < profiles.Length - 1){
            idCurrentShowedProfile++;
        }
        if(idCurrentShowedProfile == profiles.Length - 1){
            buttonContainers[0].transform.GetChild(3).gameObject.SetActive(false);
        }
        buttonContainers[0].transform.GetChild(2).gameObject.SetActive(true);
        buttonContainers[0].transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = profiles[idCurrentShowedProfile].GetLoginProfile();
    }

    private void PreviousProfile(){
        if(idCurrentShowedProfile > 0){
            idCurrentShowedProfile--;
        }
        if(idCurrentShowedProfile == 0){
            buttonContainers[0].transform.GetChild(2).gameObject.SetActive(false);
        }
        buttonContainers[0].transform.GetChild(3).gameObject.SetActive(true);
        buttonContainers[0].transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = profiles[idCurrentShowedProfile].GetLoginProfile();
    }



    private void GetProfiles(){
        DataTable profilesTable = DBConnector.GetTable("SELECT profile.id_profile, profile.login_profile FROM profile;"); 
        profiles = new Profile[profilesTable.Rows.Count];
        for(int i = 0; i < profilesTable.Rows.Count; i++){
            profiles[i] = new Profile(int.Parse(profilesTable.Rows[i][0].ToString()), profilesTable.Rows[i][1].ToString());
        }
    }

    void Update()
    {  
        timeElapsed++;
        if(idCurrentButton == 0){
            if(Input.GetKeyDown("down") || Input.GetKeyDown("tab")){
                DeactivateSprites(idCurrentButton);
                idCurrentButton = 1;
                timeElapsed = 0;
                buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
            }

            if(Input.GetKeyDown("left") && profiles.Length > 1){
                PreviousProfile();
            }
            if(Input.GetKeyDown("right") && profiles.Length > 1){
                NextProfile();
            }
        }

        if(idCurrentButton == 1){
            if(Input.GetKeyDown("up")){
                DeactivateSprites(idCurrentButton);
                idCurrentButton = 0;
                timeElapsed = 0;
                buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
            }

            if((Input.GetKeyDown("right") || Input.GetKeyDown("tab")) && profiles.Length > 0){
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
                    break;
                case 1:
                    UIMainMenuManager.instance.HideContinuePanel();
                    break;
                case 2:
                    Session.currentProfile = profiles[idCurrentShowedProfile];
                    Debug.Log("login = " +  Session.currentProfile.GetLoginProfile());
                    
                    SceneManager.LoadScene("LevelMenu");
                    break;

            }
        }

        if(Input.GetKeyDown("escape")){
            UIMainMenuManager.instance.HideContinuePanel();
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
