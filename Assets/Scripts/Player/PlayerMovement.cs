using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float minJumpHeight;
    [SerializeField] private float timeToJumpVertex;

    [SerializeField] private LayerMask abilityDespenserLayer;

    private Rigidbody2D body;
    private Transform transform;
    private PolygonCollider2D collider;
    private SpriteRenderer sprite;
    private Animator animator;
    private Vector3 localScaleVector;

    private int currentCountActions;
    private float minJumpVelocity;
    private float maxJumpVelocity;
    private float gravity;

    private Ability currentAbility;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        collider = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpVertex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpVertex;
        minJumpVelocity = Mathf.Sqrt(2*Mathf.Abs(gravity) * minJumpHeight);
        currentCountActions = 0;
        body.freezeRotation = true;
        localScaleVector = transform.localScale;
        currentAbility = null;

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private  void Update()
    {
        if(isAbilityDespenserNear()){
            Debug.Log("Оно живое");
        }

        float horizontalInput = Input.GetAxis("Horizontal") * speed;
        animator.SetFloat("horizontalSpeed", Mathf.Abs(horizontalInput));
        animator.SetFloat("verticalSpeed", body.velocity.y);
        body.velocity = new Vector2(horizontalInput, body.velocity.y);
        if(horizontalInput > 0.01f){
            sprite.flipX = false;
        } else if(horizontalInput < -0.01f){
            sprite.flipX = true;
        }
        PerformAction();
        Recover();
    }

    private bool isAbilityDespenserNear(){
        Collider2D collision = Physics2D.OverlapCircle(transform.position, 1.10f, abilityDespenserLayer); 
        if(collision != null){
            return true;
        } else {
            return false;
        }

    }


    private void Recover(){
        if(isGrounded()){
            currentCountActions = 0;
        }
    }

    private void PerformAction(){

        //TODO: временно как два разных условия
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log(currentCountActions + "после нажатия");

            //Выполняем прыжок
            if(currentCountActions == 0){
             body.velocity = new Vector2(body.velocity.x, maxJumpVelocity);
                    Debug.Log("Прыыыгаем" + currentCountActions);
             }
             if(currentCountActions == 1){
                 //Используем навык
             }
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            if(body.velocity.y > minJumpVelocity){
                body.velocity = new Vector2(body.velocity.x, minJumpVelocity);
            }
            currentCountActions++;
            Debug.Log(currentCountActions);
        }

    }

    private bool isGrounded(){
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        

        Debug.DrawRay(collider.bounds.center + new Vector3(collider.bounds.extents.x - 0.1f, 0), Vector2.down * (collider.bounds.extents.y + 0.1f), Color.green);
        return raycastHit2D.collider != null;
    }

    private void OnGameStateChanged(GameState newGameState){
        enabled = newGameState == GameState.Gameplay;
    }

    private GameObject platformGameObject;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform") ){
            //platformGameObject = null;
            transform.SetParent(collision.gameObject.transform);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform") ){
            transform.SetParent(null);
        }
    }

}
