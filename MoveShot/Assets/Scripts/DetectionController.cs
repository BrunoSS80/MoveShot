using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
   public string tagTargetDetection = "Player";

   public List<Collider2D> detectedObj = new List<Collider2D>();
   private EnemyMoviment enemyMoviment;

   private void Start() {
      
      enemyMoviment = GameObject.Find("Enemy").GetComponent<EnemyMoviment>();
      
   }
   private void OnTriggerEnter2D(Collider2D collision) {
      if(collision.gameObject.tag == tagTargetDetection){
         detectedObj.Add(collision);
         enemyMoviment.VoarPlayer();
         detectedObj.Remove(collision);
      }
   }

   /*
   private void OnTriggerExit2D(Collider2D collision) {
      if(collision.gameObject.tag == tagTargetDetection){
         detectedObj.Remove(collision);
      }
   }
   */
}
