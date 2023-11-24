using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
   public string tagTargetDetection = "Player";

   public List<Collider2D> detectedObj = new List<Collider2D>();

   private void OnTriggerEnter2D(Collider2D collision) {
      if(collision.gameObject.tag == tagTargetDetection){
         detectedObj.Add(collision);
      }
   }

   private void OnTriggerExit2D(Collider2D collision) {
      if(collision.gameObject.tag == tagTargetDetection){
         detectedObj.Remove(collision);
      }
   }
}
