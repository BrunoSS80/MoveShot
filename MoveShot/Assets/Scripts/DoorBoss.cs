using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBoss : MonoBehaviour
{   
    public SpawnManager spawnManager;
    private Transform player;
    private PlayerController playerController;
    private Animator animatorPanel;

    private void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        animatorPanel = GameObject.Find("Panel").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && spawnManager.cleanedRoom == true){
            StartCoroutine(ChangeTimer());
        }
    }

    IEnumerator ChangeTimer(){
        playerController.canMove = false;
        animatorPanel.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector2(60, 57);
        animatorPanel.SetBool("FadeOut", true);
        yield return new WaitForSeconds(1);
        animatorPanel.SetBool("FadeIn", false);
        animatorPanel.SetBool("FadeOut", false);
        playerController.canMove = true;
    }
}
