using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Collider2D boxCollider;
    public GameObject[] enemys;
    private int numEnemys;
    public int minEnemys = 3;
    public int maxEnemys = 5;
    private RoomManager roomManager;
    public bool enemysInvoked = false;
    public GameObject[] obstacles;
    public int minObstacles;
    public int maxObstacles;
    private int numObstacles;
    void Start()
    {
        boxCollider = GetComponent<Collider2D>();
        StartCoroutine(LateStart(0.1f));
    }
    
    IEnumerator LateStart(float timeWait){
        yield return new WaitForSeconds(timeWait);
        SpawnObstacles();
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
        numEnemys = Random.Range(minEnemys, maxEnemys);
        for(int i = 0; i < numEnemys; i++){
            Vector2 position = SpawnPos();
            int index = Random.Range(0, enemys.Length);
            var spawnEnemys = Instantiate(enemys[index], position, enemys[index].transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && enemysInvoked == false){
            LateStart(1.0f);
            SpawnEnemys();
        }
        enemysInvoked = true;
    }

    void SpawnObstacles(){
        numObstacles = Random.Range(minObstacles, maxObstacles);
        for(int i = 0; i < numObstacles; i++){
            Vector2 positionObstacles = SpawnPos();
            int index = Random.Range(0, obstacles.Length);
            var spawnObstacles = Instantiate(obstacles[index], positionObstacles, obstacles[index].transform.rotation);
        }
    }
}