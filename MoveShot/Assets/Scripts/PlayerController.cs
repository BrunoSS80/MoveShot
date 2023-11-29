using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]  
    private float speed = 3;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Animator playerAnimator;
    // Start is called before the first frame update
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontal, vertical);
        
        if(moveDirection.sqrMagnitude > 0){
            playerAnimator.SetInteger("Movimento", 1);
        }
        else{
            playerAnimator.SetInteger("Movimento", 0);
        }

        Flip();
    }

    private void FixedUpdate() {
        Vector3 movePosition = (speed * Time.fixedDeltaTime * moveDirection) + rb.position;

        rb.MovePosition(movePosition);
    }

    void Flip(){
        if(moveDirection.x > 0){
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if(moveDirection.x < 0){
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }
}
