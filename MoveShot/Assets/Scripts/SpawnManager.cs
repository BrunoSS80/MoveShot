using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Collider2D boxCollider;
    public GameObject[] enemys;
    private int numEnemys;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<Collider2D>();
        InvokeRepeating("SpawnEnemys", 2, 0);
    }

    Vector2 SpawnPos(){
        //Gerando posição que irá nascer o inimigo
        var bounds = boxCollider.bounds;
        var px = Random.Range(bounds.min.x, bounds.max.x);
        var py = Random.Range(bounds.min.y, bounds.max.y);
        Vector2 spawnPos = new Vector2(px, py);
        return spawnPos;
    }

    void SpawnEnemys(){
        numEnemys = Random.Range(3, 9);
        
        for(int i = 0; i < numEnemys; i++){
            Vector2 position = SpawnPos();
            int index = Random.Range(0, enemys.Length);
            Instantiate(enemys[index], position, enemys[index].transform.rotation);
        }

    }
}
