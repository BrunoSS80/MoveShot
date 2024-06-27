using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    public Transform enemy, player;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        transform.eulerAngles = new Vector3(0,0, angle);
        

        Vector3 direcaoPlayer = player.position - enemy.position;
        direcaoPlayer.z = 0;
        transform.position = enemy.position + direcaoPlayer.normalized;
    }
}
