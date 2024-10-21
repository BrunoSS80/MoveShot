using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject restartGame;

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.name == "Boss"){
            restartGame.SetActive(true);
        }
    }
}
