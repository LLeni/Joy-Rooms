using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dash : Ability {

    private float speed = 200;
    public void UseAbility(GameObject player)
    {
       player.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, player.GetComponent<Rigidbody2D>().velocity.y);
    }
}