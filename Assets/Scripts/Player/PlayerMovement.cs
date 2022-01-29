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
    private Rigidbody2D body;
    private Transform transform;
    private BoxCollider2D boxCollider;

    private int currentCountJumps;

    private float minJumpVelocity;
    private float maxJumpVelocity;
    private float gravity;

    // Start is called before the first frame update
    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpVertex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpVertex;
        minJumpVelocity = Mathf.Sqrt(2*Mathf.Abs(gravity) * minJumpHeight);
        currentCountJumps = 0;
        body.freezeRotation = true;

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void FixedUpdate() {

    }

    private  void Update()
    {
                body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        PerformJump();
        Recover();
       // Debug.Log(currentCountJumps);
    }


    private void Recover(){
            if(isGrounded()){
                currentCountJumps = 0;
            } else {

            }
    }

    private void PerformJump(){

        //TODO: временно как два разных условия
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log(currentCountJumps + "после нажатия");
            if(currentCountJumps < 2){
             body.velocity = new Vector2(body.velocity.x, maxJumpVelocity);
                    Debug.Log("Прыыыгаем" + currentCountJumps);

             }
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            if(body.velocity.y > minJumpVelocity){
                body.velocity = new Vector2(body.velocity.x, minJumpVelocity);
            }
            currentCountJumps++;
                    Debug.Log(currentCountJumps);
        }


    }

    private bool isGrounded(){
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x - 0.1f, 0), Vector2.down * (boxCollider.bounds.extents.y + 0.1f), Color.green);
        return raycastHit2D.collider != null;
    }

    private void OnGameStateChanged(GameState newGameState){
        enabled = newGameState == GameState.Gameplay;
    }

    private void OnCollisionEnter2D(Collision2D collition)
    {
        if(collition.gameObject.CompareTag("Platform")){
            transform.SetParent(collition.transform);
        }
    }

    
    private void OnCollisionExit2D(Collision2D collition)
    {
        if(collition.gameObject.CompareTag("Platform")){
            transform.SetParent(null);
        }
    
    }
}
