using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Collider2D boxCollider;
    public GameObject[] enemys;
    private int numEnemys;
    public List<GameObject> enemysList = new List<GameObject>();
    public int minEnemys = 3;
    public int maxEnemys = 5;
    public bool cleanedRoom = false;
    private RoomManager roomManager;
    public bool enemysInvoked = false;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<Collider2D>();
    }

    private void LateUpdate() {
        if(enemysList.Count == 0 && enemysInvoked == true){
            cleanedRoom = true;
        }
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
            enemysList.Add(spawnEnemys);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && cleanedRoom == false && enemysInvoked == false){
            SpawnEnemys();
        }
        enemysInvoked = true;
    }
}