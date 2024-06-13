using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_WeaponScript : MonoBehaviour
{   
    public Transform barrel;   //posição que vai sair o tiro
    public float fireRate;     //Cadencia de tiro da arma
    public GameObject bullet;  //Tiro

    private float fireTimer;   //Controle da cadencia

    private Animator weaponAnimator;
    
    void Start()
    {
        weaponAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Atirando();
    }

    void Atirando(){
        if(PodeAtirar()){
            Tiro();
        }
    }

    void Tiro(){
        fireTimer = Time.time + fireRate;
        Instantiate(bullet, barrel.position, barrel.rotation);
        weaponAnimator.SetTrigger("Fire");
    }

    private bool PodeAtirar(){
        return Time.time > fireTimer;
    } 
    }
