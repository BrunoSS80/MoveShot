using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifeTime;
    public float bulletSpeed;
    public int damageBullet;
    private Animator bulletAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        bulletAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
        bulletAnimator.SetTrigger("Fire");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            //identificando com qual inimigo colidiu e pegando seu "EnemyController"
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();

            if (enemy != null){
                enemy.DamageEnemy(damageBullet);
            }          
        }

        Destroy(gameObject);
    }
}
