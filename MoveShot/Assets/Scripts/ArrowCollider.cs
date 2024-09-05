using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour
{
    public ArrowRotation arrowRotation;
    public float timeOn = 3.5f;

    private void Start() {
        arrowRotation = GetComponentInChildren<ArrowRotation>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            StartCoroutine(TimeOn(timeOn));
        }
    }

    IEnumerator TimeOn(float time){
        arrowRotation.enabled = true;
        arrowRotation.SpriteOn();
        yield return new WaitForSeconds(time);
        arrowRotation.enabled = false;
    }
}
