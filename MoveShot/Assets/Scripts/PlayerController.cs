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
    public int heath;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void FixedUpdate() {
        Vector3 movePosition = (speed * Time.fixedDeltaTime * moveDirection) + rb.position;

        rb.MovePosition(movePosition);
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
}