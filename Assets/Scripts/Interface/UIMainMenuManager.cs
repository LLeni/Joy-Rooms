using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuManager : MonoBehaviour
{
   [SerializeField] private GameObject MainMenuPanel;
   [SerializeField] private GameObject NewGamePanel;
   [SerializeField] private GameObject ContinuePanel;

    public static UIMainMenuManager instance;
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void ShowNewGamePanel(){
        NewGamePanel.gameObject.SetActive(true);
        MainMenuPanel.gameObject.SetActive(false);
    }

    public void HideNewGamePanel(){
        NewGamePanel.gameObject.SetActive(false);
        MainMenuPanel.gameObject.SetActive(true);
    }

    public void ShowContinuePanel(){
        ContinuePanel.gameObject.SetActive(true);
        MainMenuPanel.gameObject.SetActive(false);
    }

    public void HideContinuePanel(){
        ContinuePanel.gameObject.SetActive(false);
        MainMenuPanel.gameObject.SetActive(true);
    }
}
