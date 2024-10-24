using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_MultipleBulletsSkull : MonoBehaviour
{
    [SerializeField] private Texture2D patternTexture;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform barrel;
    public float fireRate;
    public float fireTimer;
    private Animator weaponAnimator;
    public Vector2 direction;
    public AudioSource audioShotSkull;

    private void Start() {
        weaponAnimator = GetComponent<Animator>();
    }

    private void Update() {
        if(Time.time >= fireTimer + (1/ fireRate)){
            audioShotSkull.Play();
            BulletInstatiate();
            
        }
    }

    public void BulletInstatiate(){
            float speedProject = 1.0f;
            int width = patternTexture.width;
            int height = patternTexture.height;
            Vector2 centerMatriz = new Vector2(width/2, height/2);

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
        fireTimer = Time.time;
        fireRate = Random.Range(0.3f, 1);
        }
    }
