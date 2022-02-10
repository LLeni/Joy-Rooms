using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;
    private GameObject[] copyScreens;

    public static ScreenManager instance;
    public GameObject playerPrefab;

    private int numberScreen;

    void Awake()
    {
        copyScreens = new GameObject[screens.Length];
        instance = this;
        numberScreen = 0;
        copyScreens[numberScreen] = GameObject.Instantiate(screens[numberScreen], this.transform.parent);
        copyScreens[numberScreen].SetActive(true);
        Instantiate(playerPrefab, screens[numberScreen].GetComponent<ScreenContainer>().respawnPlace.position, Quaternion.identity);
    }


    public void RestartScreen(){
        Destroy(copyScreens[numberScreen]);
        copyScreens[numberScreen] = GameObject.Instantiate(screens[numberScreen], this.transform.parent);
        copyScreens[numberScreen].SetActive(true);
        Instantiate(playerPrefab, screens[numberScreen].GetComponent<ScreenContainer>().respawnPlace.position, Quaternion.identity);
    }

    public void NextScreen(){
        Destroy(copyScreens[numberScreen]);
        numberScreen++;
        screens[numberScreen].GetComponent<ScreenContainer>().respawnPlace = screens[numberScreen-1].GetComponent<ScreenContainer>().respawnPlace;
        copyScreens[numberScreen] = GameObject.Instantiate(screens[numberScreen], this.transform.parent);
        copyScreens[numberScreen].SetActive(true);

    }

    public Screen GetCurrentScreen(){
        return screens[numberScreen].GetComponent<Screen>();
    }

    public int GetNumberScreen(){
        return numberScreen;
    }

    public bool IsLastScreen(){
        if(numberScreen == screens.Length - 1){
            return true;
        } else {
            return false;
        }
    }

    public Vector2 GetPosCurrentRespawn(){
        return screens[numberScreen].GetComponent<ScreenContainer>().respawnPlace.position;
    }
}
