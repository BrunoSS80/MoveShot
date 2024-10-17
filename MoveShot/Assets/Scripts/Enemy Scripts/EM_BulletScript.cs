using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_BulletScript : MonoBehaviour
{
    public float lifeTime;
    public float bulletSpeed;
    public int em_DamageBullet;
    public int direction;

    private BoxCollider2D em_BulletColider;
    private Animator em_BulletAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        em_BulletColider = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(6,6,true);
        em_BulletAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 1){
            transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);
            em_BulletAnimator.SetTrigger("Fire");
        }
        if(direction == 2){
            transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
            em_BulletAnimator.SetTrigger("Fire");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (player != null){
                player.DamagePlayer(em_DamageBullet);
            }          
        }

        Destroy(gameObject);
    }
}
