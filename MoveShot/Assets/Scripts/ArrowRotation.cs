using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    public Transform enemy, player;
    public float timeOn = 3.5f;
    public float offset;
    public SpriteRenderer spriteArrow;
    
    private void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();
        spriteArrow = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        ArrowFPlayer();
    }

    private void ArrowFPlayer(){
        //Roração
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        transform.eulerAngles = new Vector3(0,0, angle);
        
        //Posição
        Vector3 direcaoPlayer = player.position - enemy.position;
        direcaoPlayer.z = 0;
        transform.position = enemy.position + (offset * direcaoPlayer.normalized);
    }


    public void SpriteOn(){
        StartCoroutine(Duration());
    }

    IEnumerator Duration(){
        spriteArrow.enabled = true;
        yield return new WaitForSeconds(timeOn);
        spriteArrow.enabled = false;
    }
}
