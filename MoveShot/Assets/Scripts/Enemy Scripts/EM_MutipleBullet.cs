using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EM_MutipleBullet : MonoBehaviour
{
    [SerializeField] private Texture2D patternTexture;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectMoving;
    [SerializeField] private GameObject aimBoss;
    [SerializeField] private Transform barrel;
    public float fireRate;
    private float fireTimer;
    private Animator weaponAnimator;
    public Vector2 direction;
    private Transform startPosAim, endPosAim;
    public Transform[] waypoints;
    private int currentStartPoint;
    private float startTime;
    public float speed, spawnBullet;
    private float journeyLenght;
    private void Start() {
        weaponAnimator = GetComponent<Animator>();
        currentStartPoint = 0;
        SetPoints();
    }

    private void Update() {
        if(PodeAtirar()){
            BulletInstatiate();
        }
            //for(int i = 0; i < 4; i++){
                spawnBullet -= Time.deltaTime * 6;
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLenght;

                aimBoss.transform.position = Vector2.Lerp(startPosAim.position, endPosAim.position, fracJourney);
                

                    if(fracJourney >= 1 && currentStartPoint + 1 < waypoints.Length){
                        currentStartPoint++;
                        SetPoints();
                    }
                    if(spawnBullet <= 0){
                        spawnBullet = 1;
                        Instantiate(projectMoving, aimBoss.transform.position, barrel.rotation);
                    }
                    if(currentStartPoint == 4){
                        currentStartPoint = 0;
                    }
            //}
            }
        
    
    public void BulletInstatiate(){
    //for(int i = 0; i < 5; i++){
        float speedProject = 1.0f;
        int width = patternTexture.width;
        int height = patternTexture.height;
        Vector2 centerMatriz = new Vector2(width/2, height/2);
        fireTimer = Time.time + fireRate;

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
    //}
    }

    private bool PodeAtirar(){
        return Time.time > fireTimer;
    }
    private void SetPoints(){
        startPosAim = waypoints[currentStartPoint];
        endPosAim = waypoints[currentStartPoint + 1];
        startTime = Time.time;
        journeyLenght = Vector2.Distance(startPosAim.position, endPosAim.position);
    }

    private void BulletTrail(){
    for(int i = 0; i < 4; i++){
        spawnBullet -= Time.deltaTime * 6;
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLenght;

        aimBoss.transform.position = Vector2.Lerp(startPosAim.position, endPosAim.position, fracJourney);
        fireTimer = Time.time + fireRate;

            if(fracJourney >= 1 && currentStartPoint + 1 < waypoints.Length){
                currentStartPoint++;
                SetPoints();
            }
            if(spawnBullet <= 0){
                spawnBullet = 1;
                Instantiate(projectMoving, aimBoss.transform.position, barrel.rotation);
            }
            if(currentStartPoint == 4){
                currentStartPoint = 0;
            }
    }
    fireTimer = Time.time + fireRate;
    }
}
