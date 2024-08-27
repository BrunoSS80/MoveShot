using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private CameraPlayer cameraPlayer;
    public GameObject Center;

    private void Start() {
        cameraPlayer = GameObject.Find("Main Camera").GetComponent<CameraPlayer>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            cameraPlayer.objFollow = Center;
            cameraPlayer.MoveCamera();
        }
    }
}
