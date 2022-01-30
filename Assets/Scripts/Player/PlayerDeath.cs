using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collition)
    {
        if(collition.gameObject.CompareTag("Enemy") || collition.gameObject.CompareTag("PainfulTrap")){
            Destroy(gameObject);
            ScreenManager.instance.RestartScreen();
        }
    }
}
