using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
   public string tagTargetDetection = "Player";
   public List<Collider2D> detectedObj = new List<Collider2D>();
   private GameObject enemys;
   private GameObject enemyMovPai;

   private void Start() {  
      enemyMovPai = transform.parent.gameObject;
   }

   private void OnTriggerEnter2D(Collider2D collision) {
      if(collision.gameObject.tag == tagTargetDetection){
         detectedObj.Add(collision);
         enemyMovPai.GetComponent<EnemyMoviment>().VoarPlayer();
         detectedObj.Remove(collision);
      }
   }
   
}
