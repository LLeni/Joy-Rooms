using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float baseJumpPower;
    [SerializeField] private LayerMask groundLayer;
    private float maxJumpHeight = 4;
    private float minJumpHeight = 1;

    private float timeToJumpVertex = .4f;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;

    private float timeLeftForJumping;
    private float jumpPower;
    private bool isPerformingJump;

    private float minJumpVelocity;
    private float maxJumpVelocity;
    private float gravity;

    // Start is called before the first frame update
    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpVertex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpVertex;
        minJumpVelocity = Mathf.Sqrt(2*Mathf.Abs(gravity) * minJumpHeight);
    }

    private void Update() {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        PerformJump();
    }

    private void PerformJump(){
        // if(timeLeftForJumping > 0 && !isGrounded()){
        //     timeLeftForJumping -= Time.deltaTime;
        // }

        if(Input.GetKey(KeyCode.Space) && isGrounded() ){
            timeLeftForJumping = timeToJumpVertex;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded()){
            //if(timeLeftForJumping > 0){
                Debug.Log("asdasd");
                body.velocity = new Vector2(body.velocity.x, maxJumpVelocity);
            //}


            // if(isGrounded()){
            //     timeLeftForJumping = 1.0f/60; // кадр
            //     jumpPower = 0;
            //     isPerformingJump = true;
            //     body.velocity = new Vector2(body.velocity.x, jumpSpeed); //задаем первоначальную скорость для единичного нажатия
            // } 
            // if(timeLeftForJumping > 0 && isPerformingJump){
            //     jumpPower += baseJumpPower;
            //     body.velocity = new Vector2(body.velocity.x, body.velocity.y + jumpSpeed * jumpPower);
            // }
        }  else {
            isPerformingJump = false;
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            Debug.Log("heyhey");
            if(body.velocity.y > minJumpVelocity){
                body.velocity = new Vector2(body.velocity.x, minJumpVelocity);
            }
        }


    }

    private bool isGrounded(){
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.05f, groundLayer);
        return raycastHit2D.collider != null;
    }
}
