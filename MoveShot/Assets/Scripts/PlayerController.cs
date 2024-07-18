using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerController : MonoBehaviour
{
    [SerializeField]  
    private float speed = 3;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Animator playerAnimator;
    public int heath;
    private SpriteRenderer spriteRenderer;
    public float rollSpeed = 100f;
    private Vector3 rollDir;
    private enum State{
        Normal,
        Rolling,
    }
    private State state;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.Normal;
    }

    private void Update() {
        switch(state){
            case State.Normal:
            WalkPlayer();
            ActiveRoll();
            break;
            
            case State.Rolling:
            RollingPL();
            break;
        }
    }
    private void FixedUpdate() {
        switch(state){
            case State.Normal:
            MovePlayerDir();
            break;
        }
    }

    private void WalkPlayer(){
        Vector3 movePosition = (speed * Time.fixedDeltaTime * moveDirection) + rb.position;
        rb.MovePosition(movePosition);
    }
    public void MovePlayerDir(){
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontal, vertical);
        
        playerAnimator.SetFloat("Horizontal", direction.x);
        playerAnimator.SetFloat("Vertical", direction.y);
        playerAnimator.SetFloat("Speed", moveDirection.sqrMagnitude);
    }

    public void DamagePlayer(int em_damageBullet){
        heath -= em_damageBullet;
        StartCoroutine(DamagePlayer());

        if(heath <1){
            Destroy(gameObject);
        }
    }
    IEnumerator DamagePlayer(){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    public void ActiveRoll(){
        if(Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)){
            Vector3 mousePos = Input.mousePosition;
            rollDir = (Camera.main.ScreenToWorldPoint(mousePos) - transform.position).normalized;
            rollSpeed = 50;
            playerAnimator.SetTrigger("Roll");
            state = State.Rolling;
        }
    }
    private void RollingPL(){
        transform.position += rollDir * rollSpeed * Time.deltaTime;
        rollSpeed -= rollSpeed * 10 * Time.deltaTime;
        if(rollSpeed <= 5){
        float collDownRoll = 0.5f;
        StartCoroutine(CollDown(collDownRoll));
        }
    }

    IEnumerator CollDown(float timeToWait){
        yield return new WaitForSeconds(timeToWait);
        state = State.Normal;
    }
}
