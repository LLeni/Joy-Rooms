using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundChecker;

    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Transform transform;

    private bool isWalking;
    private bool isTurning;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform =  GetComponent<Transform>();
        speed = 100;
        isWalking = true;
        isTurning = false;

        //GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if(isWalking){
            Walk();
        }
    }

    void FixedUpdate()
    {
        if(isWalking){
            isTurning = !Physics2D.OverlapCircle(groundChecker.position, 0.1f, groundLayer);
        }
    }

    private void Walk(){

        if(isTurning){
            Flip();
        }
        rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
    }

    private void Flip(){
        isWalking = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        isWalking = true;
    }

    private void OnGameStateChanged(GameState newGameState){
        enabled = newGameState == GameState.Gameplay;
    }
}
