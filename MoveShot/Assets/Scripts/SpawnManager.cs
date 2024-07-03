using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Collider2D boxCollider;
    public List<GameObject> enemys = new List<GameObject>();
    private int numEnemys;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<Collider2D>();
        numEnemys = Random.Range(3, 9);
        
    }

    // Update is called once per frame
    void Update()
    {
        var bounds = boxCollider.bounds;
        var px = Random.Range(bounds.min.x, bounds.max.x);
        var py = Random.Range(bounds.min.y, bounds.max.y);
        Debug.Log(px + "," + py);
    }
}
