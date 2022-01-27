﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collition)
    {
        if(collition.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
            ScreenManager.instance.RestartScreen();
        }
    }
}