using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private Transform player;
    public Vector3 walkTo;

    private void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            player.transform.position = Vector2.Lerp (player.transform.position, walkTo, Time.deltaTime);
        }
    }
}
