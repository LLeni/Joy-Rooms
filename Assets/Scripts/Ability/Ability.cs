using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void UseAbility(GameObject player);
}