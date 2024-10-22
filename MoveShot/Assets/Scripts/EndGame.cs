using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Canvas restartGame;

    private void Start() {
        restartGame = GameObject.Find("Screen").GetComponent<Canvas>();
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Boss")){
            restartGame.gameObject.GetComponent<Canvas>().enabled = true;
        }
    }
}
