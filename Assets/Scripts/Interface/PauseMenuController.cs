using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    [SerializeField] private GameObject[] buttonContainers;
    [SerializeField] private float timeForAnimation;
    private bool isActive;
    private int idCurrentButton = 0;

    private float timeElapsed;

    // Start is called before the first frame update
    void Awake()
    {
        isActive = false;
        idCurrentButton = 0;
        timeElapsed = 0;
        buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {  
        timeElapsed++;
        if(Input.GetKeyDown("down")){
            DeactivateSprites(idCurrentButton);
            if(idCurrentButton == buttonContainers.Length - 1){
                idCurrentButton = 0;
            } else {
                idCurrentButton++;
            }
            timeElapsed = 0;
            buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
        }
        if(Input.GetKeyDown("up")){
            DeactivateSprites(idCurrentButton);
            if(idCurrentButton == 0){
               idCurrentButton = buttonContainers.Length - 1;
            } else {
                idCurrentButton--;
            }
            timeElapsed = 0;
            buttonContainers[idCurrentButton].transform.GetChild(0).gameObject.SetActive(true);
        }
        if(Input.GetKeyDown("return")){
            switch(idCurrentButton){
                case 0:
                    GameManager.instance.ResumeGame();
                    UIManager.instance.HidePauseMenu();
                    break;
                case 1:
                    GameManager.instance.ResumeGame();
                    UIManager.instance.HidePauseMenu();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                case 2:
                    break;
                case 3:
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
