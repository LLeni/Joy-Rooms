using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondJump : Ability
{
    private float speed = 12;
    public void UseAbility(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, speed);
    }
}
