using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDespenser : MonoBehaviour
{

    [SerializeField] private AbilityName abilityName;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Sprite[] spriteArray;
    private Ability ability;
    private SpriteRenderer spriteRenderer;
    enum AbilityName {
        Dash,
        SecondJump,
        None,
        Refresh
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch(abilityName){
            case AbilityName.Dash: 
                Debug.Log("куку");
                ability = new Dash();
                spriteRenderer.sprite = spriteArray[0];
                break;
            case AbilityName.SecondJump:
                ability = new SecondJump();
                spriteRenderer.sprite = spriteArray[1];
                break;
            case AbilityName.None:
                ability = null;
                spriteRenderer.sprite = spriteArray[2];
                break;
            case AbilityName.Refresh:
                //перезаряжаем текущий навык
                spriteRenderer.sprite = spriteArray[3];
                break;
        }

        Debug.Log("ability check = " + (ability == null));

        Debug.Log("check dash: " + (new Dash() == null));
    }



    public Ability GetAbility(){
        Debug.Log("kinda ability = " + (ability == null));
        return ability;
    }
    
}
