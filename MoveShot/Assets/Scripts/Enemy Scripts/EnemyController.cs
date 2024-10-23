using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int heath;
    public Image imageLifeBoss;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        imageLifeBoss = GameObject.Find("LifeBossBar").GetComponent<Image>();
    }


    public void DamageEnemy(int damageBullet){
        heath -= damageBullet;
        StartCoroutine(Damage());
            if(gameObject.CompareTag("Boss")){
                imageLifeBoss.gameObject.GetComponent<Image>().fillAmount -= 0.04f;
            }
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
