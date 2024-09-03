using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerController : MonoBehaviour
{
    [SerializeField]  
    private float speed = 3;
    private CapsuleCollider2D colliderPlayer;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Animator playerAnimator;
    public int heath;
    private SpriteRenderer spriteRenderer;
    public float rollSpeed = 75f;
    private Vector3 rollDir;
    public float coolDownRoll = 3f;
    public float resetRoll = 3f;
    private bool canRoll = true;
    public bool canMove = true;
    private enum State{
        Normal,
        Rolling,
    }
    private State state;
    public Color colorResetRoll;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderPlayer = GetComponent<CapsuleCollider2D>();
        state = State.Normal;
    }

    private void Update() {
        if(canMove == true){
            switch(state){
                case State.Normal:
                WalkPlayer();
                ActiveRoll();
                break;
            }
            switch(state){
                case State.Rolling:
                RollingPL();
                break;
            }
        }
        if(canRoll == false){
            coolDownRoll -= Time.deltaTime;
            
            if(coolDownRoll <= 0){
                coolDownRoll = resetRoll;
                canRoll = true;
                ResetRoll();
            }
        }
    }
    private void FixedUpdate() {
        if(canMove == true){
            switch(state){
                case State.Normal:
                MovePlayerDir();
                break;
            }
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
        if(canRoll == true){
            if(Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)){
                playerAnimator.SetFloat("Horizontal", 0);
                playerAnimator.SetFloat("Vertical",0);
                Vector3 mousePos = Input.mousePosition;
                rollDir = (Camera.main.ScreenToWorldPoint(mousePos) - transform.position).normalized;
                if(rollDir.x > 0.01f){
                    playerAnimator.SetTrigger("Roll_Right");
                }
                else{
                    playerAnimator.SetTrigger("Roll_Left");
                }
                rollSpeed = 50;
                colliderPlayer.enabled = false;
                state = State.Rolling;
            }
        }
    }
    private void RollingPL(){
        transform.position += rollDir * rollSpeed * Time.deltaTime;
        rollSpeed -= rollSpeed * 10 * Time.deltaTime;
        if(rollSpeed <= 5){
        playerAnimator.SetFloat("Horizontal", 1);
        canRoll = false;
        state = State.Normal;
        transform.position = new Vector3(transform.position.x, transform.position.y , 0);
        colliderPlayer.enabled = true;
        }
    }
    private void ResetRoll(){
        StartCoroutine(ResetRollC());
    }
    IEnumerator ResetRollC(){
        spriteRenderer.color = colorResetRoll;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
