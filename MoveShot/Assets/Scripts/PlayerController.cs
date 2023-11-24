using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]  
    private float speed = 3;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    // Start is called before the first frame update
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontal, vertical);
        
    }

    private void FixedUpdate() {
        Vector3 movePosition = (speed * Time.fixedDeltaTime * moveDirection) + rb.position;

        rb.MovePosition(movePosition);
    }
}
