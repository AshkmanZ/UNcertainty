using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    Rigidbody2D rb;
    Vector2 movement;

    public bool canMove;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    private void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
