using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_BulletScript : MonoBehaviour
{
    public float lifeTime;
    public float bulletSpeed;
    public int em_DamageBullet;

    private BoxCollider2D em_BulletColider;
    private Animator em_BulletAnimator;
    public EM_MutipleBullet eM_MutipleBullet;
    private Rigidbody2D rb_Bullet;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        eM_MutipleBullet = GameObject.Find("Boss").GetComponent<EM_MutipleBullet>();
        em_BulletColider = GetComponent<BoxCollider2D>();
        rb_Bullet = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6,6,true);
        em_BulletAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(eM_MutipleBullet.direction * bulletSpeed * Time.deltaTime);
        //rb_Bullet.AddForce(eM_MutipleBullet.direction * bulletSpeed, ForceMode2D.Impulse);
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
