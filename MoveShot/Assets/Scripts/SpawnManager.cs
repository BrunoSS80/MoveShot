using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Collider2D boxCollider;
    public GameObject[] enemys;
    public List<int> enemysAlive;
    public GameObject roomCurrent;
    private int numEnemys;
    public int minEnemys = 3;
    public int maxEnemys = 5;
    private RoomManager roomManager;
    public bool enemysInvoked = false;
    public GameObject[] obstacles;
    public int minObstacles;
    public int maxObstacles;
    private int numObstacles;
    public bool cleanedRoom, spawningEnemys, marksSpawned = false;
    public GameObject markSpawn;
    public List<Vector2> positionsMarks;
    public List<GameObject> marks;
    public ParticleSystem particlesMark;
    void Start()
    {
        boxCollider = GetComponent<Collider2D>();
        numEnemys = Random.Range(minEnemys, maxEnemys);
        StartCoroutine(LateStart(0.1f));
    }
    
    IEnumerator LateStart(float timeWait){
        for(int i = 0; i < numEnemys; i++){
        Vector2 positionMark = SpawnPos();
        positionsMarks.Add(positionMark);
        }
        yield return new WaitForSeconds(timeWait);
        SpawnObstacles();
    }
    private void Update() {
        if(enemysAlive.Count <= 0 && spawningEnemys == true){
            cleanedRoom = true;
        }
        else{
            cleanedRoom = false;
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
    void SpawnMark(){
        for(int x = 0; x < numEnemys; x++){
            var mark = Instantiate(markSpawn, positionsMarks[x], markSpawn.transform.rotation);
            marks.Add(mark);
        }
    }

    void SpawnEnemys(){
        for(int i = 0; i < numEnemys; i++){
            int index = Random.Range(0, enemys.Length);
            var spawnEnemys = Instantiate(enemys[index], positionsMarks[i], enemys[index].transform.rotation);
            enemysAlive.Add(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && enemysInvoked == false && spawningEnemys == false && marksSpawned == false){
            marksSpawned = true;
            StartCoroutine(WaitSeconds(1.5f, 2));
        }
    }
    IEnumerator WaitSeconds(float timeStart, float timeDestroy){
        yield return new WaitForSeconds(timeStart);
        SpawnMark();
        yield return new WaitForSeconds(timeDestroy);
        for(int i = 0; i < marks.Count; i++){
        Instantiate(particlesMark, positionsMarks[i], Quaternion.identity);
        Destroy(marks[i]);
        }
        SpawnEnemys();
        spawningEnemys = true;
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

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){
            enemysAlive.Remove(1);
        }
    }
}