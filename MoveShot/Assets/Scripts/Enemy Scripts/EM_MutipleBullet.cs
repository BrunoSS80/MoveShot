using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EM_MutipleBullet : MonoBehaviour
{
    [SerializeField] private Texture2D patternTexture;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectMoving, projectMovingLeft;
    [SerializeField] private GameObject aimBoss, aimBossLeft;
    [SerializeField] private Transform barrel;
    [SerializeField]private Transform barrelInverse;
    public float fireRate;
    public float fireTimer;
    private Animator weaponAnimator;
    public Vector2 direction;
    private Transform startPosAim, endPosAim, startPosAimLeft, endPosAimLeft;
    public Transform[] waypoints, wayPointsLeft;
    public int currentStartPoint, timesExecuted, currentStartPointLeft;
    public int randomHability;
    private float startTime;
    public float speed, spawnBullet;
    private float journeyLenght, journeyLenghtLeft;
    public AudioSource audioExplosion, audioShot;
    private void Start() {
        weaponAnimator = GetComponent<Animator>();
        currentStartPoint = 0;
        SetPoints();
    }

    private void Update() {
        if(Time.time >= fireTimer + (1/ fireRate)){
            randomHability = Random.Range(0,2);
            if(randomHability == 0){
                BulletInstatiate();
            }
        }
            if(randomHability == 1 && timesExecuted < 2){
                spawnBullet -= Time.deltaTime * 6;
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLenght;
                float fracJourneyLeft = distCovered / journeyLenghtLeft;

                aimBoss.transform.position = Vector2.Lerp(startPosAim.position, endPosAim.position, fracJourney);
                aimBossLeft.transform.position = Vector2.Lerp(startPosAimLeft.position, endPosAimLeft.position, fracJourneyLeft);
                
                    if(fracJourney >= 1 && currentStartPoint + 1 < waypoints.Length){
                        currentStartPoint++;
                        SetPoints();
                    }
                    if(spawnBullet <= 0){
                        spawnBullet = 1;
                        Instantiate(projectMoving, aimBoss.transform.position, barrel.rotation);
                        barrelInverse.rotation = Quaternion.Inverse(barrel.rotation);
                        Instantiate(projectMovingLeft, aimBossLeft.transform.position, barrelInverse.rotation);
                        audioShot.Play();
                    }
                    if(currentStartPoint >= 4){
                        currentStartPoint = 0;
                        timesExecuted++;
                    }

                    if(fracJourney >= 1 && currentStartPointLeft + 1 < wayPointsLeft.Length){
                        currentStartPointLeft++;
                        SetPoints();
                    }
                fireTimer = Time.time;
            }
        if(timesExecuted >= 3){
            ResetCount();
        }
    }
    
    public void BulletInstatiate(){
        if(timesExecuted <= 2){
            float speedProject = 1.0f;
            int width = patternTexture.width;
            int height = patternTexture.height;
            Vector2 centerMatriz = new Vector2(width/2, height/2);
            audioExplosion.Play();

                for(int y = 0; y < height; y++){
                    for(int x = 0; x < width; x++){
                        Color pixelColor = patternTexture.GetPixel(x,y);
                        
                        if(pixelColor == Color.black){
                            Vector2 position = barrel.position + (new Vector3(x - width/2, y - height/2));
                            direction = new Vector2(x,y) - centerMatriz;
                            Rigidbody2D projectile = Instantiate(projectilePrefab, position, barrel.rotation).GetComponent<Rigidbody2D>();
                            projectile.velocity = direction * speedProject;
                            weaponAnimator.SetTrigger("Fire");
                        }
                    }
                }
            timesExecuted++;
        }
        fireTimer = Time.time;
    }

    private void SetPoints(){
        startPosAim = waypoints[currentStartPoint];
        endPosAim = waypoints[currentStartPoint + 1];

        startPosAimLeft = wayPointsLeft[currentStartPoint];
        endPosAimLeft = wayPointsLeft[currentStartPoint + 1];
        
        startTime = Time.time;
        journeyLenght = Vector2.Distance(startPosAim.position, endPosAim.position);
        journeyLenghtLeft = Vector2.Distance(startPosAimLeft.position, endPosAimLeft.position);
    }
    private void ResetCount(){
        timesExecuted = 0;
    }
}
