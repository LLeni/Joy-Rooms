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
        copyScreens[numberScreen] = GameObject.Instantiate(screens[numberScreen]);
        Debug.Log("Хей");
        screens[0].SetActive(true);
    }

    //Для сохранения первоначального состояния экранов
    // private void DeepCloneScreen(GameObject source, GameObject target){
    //     MemoryStream stream = new MemoryStream();
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     formatter.Serialize(stream, source);
    //     stream.Position = 0;
    //     target = (GameObject) formatter.Deserialize(stream);
    // }

    // private void ShowFirstScreen(Scene scene, LoadSceneMode mode){
    //     currentScreen = screens[currentScreen.number + 1].GetComponent<Screen>();
    //     screens[0].SetActive(true);
    // }

    public void RestartScreen(){
        //Instantiate(playerPrefab, screens[numberScreen].GetComponent<Screen>().respawnPlace.position, Quaternion.identity);
        Debug.Log("copyScreens [concrete] = " + copyScreens[numberScreen] );
        copyScreens[numberScreen].SetActive(true);
       copyScreens[numberScreen] = GameObject.Instantiate(copyScreens[numberScreen]);
               Debug.Log("copyScreens [concrete] = " + copyScreens[numberScreen] );
        
    }

    public void NextScreen(){
        if(numberScreen == screens.Length - 1){
            Debug.Log("EndLevelGG");
        } else{
            screens[numberScreen].SetActive(false);
            numberScreen++;
            screens[numberScreen].GetComponent<Screen>().respawnPlace = screens[numberScreen-1].GetComponent<Screen>().respawnPlace;
            copyScreens[numberScreen] = GameObject.Instantiate(screens[numberScreen]);
            screens[numberScreen].SetActive(true);
            //currentScreen = screens[currentScreen.number + 1].GetComponent<Screen>().respawnPlace.position;
        }

    }
}
