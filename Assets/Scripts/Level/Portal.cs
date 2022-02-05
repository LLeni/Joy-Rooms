using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float timeToRotate;
    private float timeElapsed;
    private bool isUp;

    void Start(){
        timeElapsed = 0;
        isUp = false;

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void Update()
    {
        timeElapsed++;
        if(timeElapsed == timeToRotate){
            transform.Rotate(Vector3.forward * -10);
            // if(isUp){
            //     transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            //     isUp = false;
            // } else {
            //     transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            //     isUp = true;
            // }
            timeElapsed = 0;
        }

    }

    private void OnGameStateChanged(GameState newGameState){
        enabled = newGameState == GameState.Gameplay;
    }
}
