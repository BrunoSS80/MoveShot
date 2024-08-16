using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private Transform player;
    private PlayerController playerController;
    private float durationWalk = 5;
    public Animator animatorPanel;
    public float valorX, valorY;

    private void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        animatorPanel = GameObject.Find("Panel").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            StartCoroutine(ChangeTimer());
        }
    }

    IEnumerator ChangeTimer(){
        playerController.canMove = false;
        animatorPanel.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1);
        player.transform.position = Vector2.Lerp(player.transform.position, player.transform.position + new Vector3(valorX, valorY), durationWalk);
        animatorPanel.SetBool("FadeOut", true);
        yield return new WaitForSeconds(1);
        animatorPanel.SetBool("FadeIn", false);
        animatorPanel.SetBool("FadeOut", false);
        playerController.canMove = true;
    }
}
