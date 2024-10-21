using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraPlayer : MonoBehaviour
{
    public float dampTime = 0.15f;
    public GameObject objFollow;
    
    void Update () {
        
    }

    public void MoveCamera(){
        transform.position = Vector3.Lerp(transform.position, objFollow.transform.position, 1);
    }

    public void RestartGame(){
        SceneManager.LoadScene("SampleScene");
        Debug.Log("as");
    }
}
