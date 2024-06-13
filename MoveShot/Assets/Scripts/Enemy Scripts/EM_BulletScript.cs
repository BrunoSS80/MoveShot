using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_BulletScript : MonoBehaviour
{
   public float lifeTime;
    public float bulletSpeed;
    public int em_DamageBullet;

    private BoxCollider2D em_BulletColider;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        em_BulletColider = GetComponent<BoxCollider2D>();

        Physics2D.IgnoreLayerCollision(7,7,true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);
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
