using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    public float speedEnemy = 3.5f;
    public float distanceDash = 10.0f;
    private Vector2 enemydirection;
    private Rigidbody2D enemyRB;
    private SpriteRenderer spriteRenderer;
    public DetectionController detectionController;
    public Transform player;
    

    // Start is called before the first frame update
    void Start() {
        enemyRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        
        if(player.transform.position == transform.position){
            Debug.Log("parou");
        }

        if(enemydirection.x > 0){
            spriteRenderer.flipX = false;
        }
        if(enemydirection.x < 0){
            spriteRenderer.flipX = true;
        }
    }

    public void VoarPlayer(){
        StartCoroutine(Espera());
        //transform.position += (Vector3)enemydirection * distanceDash;
    }
    IEnumerator Espera(){
        transform.position += transform.position * 0;
        yield return new WaitForSeconds(5.0f);
        transform.position = player.transform.position;
    }
}