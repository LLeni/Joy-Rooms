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

    void Update(){
        CheckAbilityDespeserHit();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch(abilityName){
            case AbilityName.Dash: 
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



    }

    private void CheckAbilityDespeserHit(){
        Collider2D hit = Physics2D.OverlapBox(transform.position, new Vector2(1,1), 0, playerLayer);
  
        if(hit != null){
            UIManager.instance.SetAbilityImage(spriteRenderer.sprite);
            Debug.Log("DDDDD");
            
        }
    }



    public Ability GetAbility(){
        return ability;
    }
    
}
