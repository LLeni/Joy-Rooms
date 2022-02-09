
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimator : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private int amountImages;

    [SerializeField] private int speedAnimation;

    private Image[] playerImages;
    void Start()
    {
        playerImages = new Image[amountImages];

        for(int i = 0; i < playerImages.Length; i++){
            playerImages[i] = GameObject.Instantiate(playerImage);
            playerImages[i].transform.parent = this.transform;
        }
    }

    void Update()
    {
        for(int i = 0; i < playerImages.Length; i++){
            if (playerImages[i].gameObject.transform.position.x > Screen.width  || 
                playerImages[i].gameObject.transform.position.x < 0 || 
                playerImages[i].gameObject.transform.position.y > Screen.height + 100 || 
                playerImages[i].gameObject.transform.position.y < -Screen.height){
                    playerImages[i].gameObject.transform.position = new Vector2(Random.Range(0, Screen.width), Random.Range(-Screen.height,0));

            }
            playerImages[i].gameObject.transform.position += new Vector3(0, speedAnimation, 0);
        }
    }
}
