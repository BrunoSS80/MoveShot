using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_HitPlayer : MonoBehaviour
{
    private PlayerController playerController;

    private void Start() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        playerController.DamagePlayer(1);
    }
}
