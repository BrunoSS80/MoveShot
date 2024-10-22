using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int heath;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void DamageEnemy(int damageBullet){
        heath -= damageBullet;
        StartCoroutine(Damage());

        if(heath < 1){
            Destroy(gameObject);
        }
    }

    IEnumerator Damage(){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
