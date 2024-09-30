using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class EM_MutipleBullet : MonoBehaviour
{
    [SerializeField] private Texture2D patternTexture;
    [SerializeField] private GameObject projectilePrefab;
    private EM_WeaponScript eM_WeaponScript;
    [SerializeField] private Transform barrel;
    public float fireRate;
    private float fireTimer;
    private Animator weaponAnimator;
    public Vector2 direction;
    
    private void Start() {
        weaponAnimator = GetComponent<Animator>();
    }

    private void Update() {
        if(PodeAtirar()){
            BulletInstatiate();
        }
    }
    public void BulletInstatiate(){
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

                //Rigidbody2D projectile = Instantiate(projectilePrefab, position, barrel.rotation).GetComponent<Rigidbody2D>();
                Rigidbody2D projectile = Instantiate(projectilePrefab, position, Quaternion.identity).GetComponent<Rigidbody2D>();
                projectile.velocity = direction * speedProject;
                

                /*
                projectile.velocity = direction * speedProject * Time.deltaTime;
                weaponAnimator.SetTrigger("Fire");
                */
            }
        }
    }
    }

    private bool PodeAtirar(){
        return Time.time > fireTimer;
    }
}
