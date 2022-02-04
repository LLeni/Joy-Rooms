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
        Instantiate(playerPrefab, screens[numberScreen].GetComponent<Screen>().respawnPlace.position, Quaternion.identity);
    }


    public void RestartScreen(){
        Destroy(copyScreens[numberScreen]);
        copyScreens[numberScreen] = GameObject.Instantiate(screens[numberScreen], this.transform.parent);
        copyScreens[numberScreen].SetActive(true);
        Instantiate(playerPrefab, screens[numberScreen].GetComponent<Screen>().respawnPlace.position, Quaternion.identity);
    }

    public void NextScreen(){
        if(numberScreen == screens.Length - 1){
            Debug.Log("EndLevelGG");
        } else{
            Destroy(copyScreens[numberScreen]);
            numberScreen++;
            screens[numberScreen].GetComponent<Screen>().respawnPlace = screens[numberScreen-1].GetComponent<Screen>().respawnPlace;
            copyScreens[numberScreen] = GameObject.Instantiate(screens[numberScreen], this.transform.parent);
            copyScreens[numberScreen].SetActive(true);
        }

    }
}
