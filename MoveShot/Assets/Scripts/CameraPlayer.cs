using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public float dampTime = 0.15f;
    public GameObject objFollow;
    
    void Update () {
        
    }

    public void MoveCamera(){
        transform.position = Vector3.Lerp(transform.position, objFollow.transform.position, 1);
    }
}
