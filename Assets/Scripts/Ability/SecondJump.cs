using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondJump : Ability
{
    private float speed = 10;
    public override void UseAbility(GameObject player)
    {
        
        if(Input.GetKey(KeyCode.LeftShift))
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, player.GetComponent<Rigidbody2D>().velocity.y);
        Debug.Log("123123123");
    }
}
