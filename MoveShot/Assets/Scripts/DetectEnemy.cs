using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    public bool cleanedRoom = false;
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){
            cleanedRoom = false;
        }
        else{
            cleanedRoom = true;
        }
    }
}
