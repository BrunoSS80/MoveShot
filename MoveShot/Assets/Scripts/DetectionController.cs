using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
   public string tagTargetDetection = "Player";

   public List<Collider2D> detectedObj = new List<Collider2D>();
   private EnemyMoviment enemyMoviment;
   private ArrowRotation arrowRotation;

   private void Start() {
      
      enemyMoviment = GameObject.Find("Enemy").GetComponent<EnemyMoviment>();
      arrowRotation = GameObject.Find("ArrowToPlayer").GetComponent<ArrowRotation>();
      
   }
   private void OnTriggerEnter2D(Collider2D collision) {
      if(collision.gameObject.tag == tagTargetDetection){
         detectedObj.Add(collision);
         enemyMoviment.VoarPlayer();
         arrowRotation.SpriteOn();
         detectedObj.Remove(collision);
      }
   }
}
