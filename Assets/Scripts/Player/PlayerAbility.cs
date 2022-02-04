using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{

    [SerializeField] private LayerMask abilityDespenserLayer;
    private Ability ability;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position, abilityDespenserLayer);

        if(hit != null){
            ability = hit.gameObject.GetComponent<AbilityDespenser>().GetAbility();
            Debug.Log("in update");
                    Debug.Log(ability == null);
        }

        if(ability != null){
            ability.UseAbility(this.gameObject);
        }
    }
}
