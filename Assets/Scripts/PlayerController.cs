using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public float speed = 1.0f;
    public LayerMask interactLayer;
    public float interactDis = 1.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player Movement
        rb.velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            sprite.flipY = false;
            rb.velocity += Vector2.right * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += Vector2.down * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            sprite.flipY = true;
            rb.velocity += Vector2.left * speed;
        }
        
        //Interact With Object
        if (Input.GetMouseButton(0))
        {
            Vector2 facing = transform.right;
            if (sprite.flipY == true) facing *= -1;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, facing, interactDis, interactLayer.value);
            if (hit)
            {
                //do something with interaction
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}
