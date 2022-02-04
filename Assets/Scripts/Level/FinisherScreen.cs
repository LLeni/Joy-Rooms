using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherScreen : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;

    void Update()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, 0.01f, playerMask); 
        if(collision != null){
            ScreenManager.instance.NextScreen();
        }
    }
}
