using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    public float speedEnemy = 3.5f;
    public float distanceDash = 0.5f;
    private Vector2 enemydirection;
    private Rigidbody2D enemyRB;
    private SpriteRenderer spriteRenderer;
    public DetectionController detectionController;
    public Transform player;
    

    // Start is called before the first frame update
    void Start() {
        enemyRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        enemydirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate() {
        
        //enemydirection = (detectionController.detectedObj[0].transform.position - transform.position).normalized;
        enemydirection = (player.transform.position - transform.position).normalized;
        enemyRB.MovePosition(enemyRB.position + enemydirection * speedEnemy * Time.fixedDeltaTime);

        if(enemydirection.x > 0){
            spriteRenderer.flipX = false;
        }
        if(enemydirection.x < 0){
            spriteRenderer.flipX = true;
        }
    }

    
    public void VoarPlayer(){
        StartCoroutine(Espera());
    }
    IEnumerator Espera(){
        speedEnemy = 0;
        yield return new WaitForSeconds(3.0f);
        Vector2 playerPos = player.transform.position;
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector2.MoveTowards(transform.position, playerPos, distanceDash / 2.0f);
        speedEnemy = 3.5f;
        Debug.Log("Foi");
    }
    
}