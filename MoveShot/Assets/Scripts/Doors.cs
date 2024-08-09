using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private Transform player;
    public Vector3 walkTo;
    private float durationWalk = 5;
    public Animator animatorPanel;

    private void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();
        animatorPanel = GameObject.Find("Panel").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            StartCoroutine(ChangeTimer());
        }
    }

    IEnumerator ChangeTimer(){
        animatorPanel.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1);
        player.transform.position = Vector2.Lerp(player.transform.position, walkTo, durationWalk);
        animatorPanel.SetBool("FadeOut", true);
        yield return new WaitForSeconds(1);
        animatorPanel.SetBool("FadeIn", false);
        animatorPanel.SetBool("FadeOut", false);
    }
}
