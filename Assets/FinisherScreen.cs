using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherScreen : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collition)
    {
        if(collition.gameObject.CompareTag("Player")){
            ScreenManager.instance.NextScreen();
        }
    }
}
