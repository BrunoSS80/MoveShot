using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int heath;
    private SpawnManager spawnManager;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    public void DamageEnemy(int damageBullet){
        heath -= damageBullet;
        StartCoroutine(Damage());

        if(heath < 1){
            spawnManager.enemysList.RemoveAt(0);
            Destroy(gameObject);
        }
    }

    IEnumerator Damage(){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
