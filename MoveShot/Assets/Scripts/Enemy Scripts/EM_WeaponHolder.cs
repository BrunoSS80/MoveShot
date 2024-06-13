using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_WeaponHolder : MonoBehaviour
{
    public Transform enemy;
    public float offset;
    
    public Transform alvo;
    

    // Update is called once per frame
    void Update()
    {
        RodarArma();
    }

    void RodarArma(){
        //Rotação
        var dir = alvo.position - transform.position;
        var angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0, angle);

        //Posição
        Vector3 emToPlayer = Camera.main.ScreenToWorldPoint(alvo.position) - enemy.position;
        emToPlayer.z = 0;
        transform.position = enemy.position + (offset * emToPlayer.normalized);

        //Girar
        Vector3 localScale = Vector3.one;

        if(angle > 90 || angle < -90){
            localScale.y = -1f;
        }
        else{
            localScale.y = 1f;
        }
        transform.localScale = localScale;
    }
}
