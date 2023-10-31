using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private AudioSource audioSource;
    Animator anim;
    public float speed = 1.0f;
    public LayerMask interactLayer;
    public float interactDis = 1.0f;
    public bool[] prevInputs = new bool[]{false, false, false, false}; //wasd
    public Sprite[] standing;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
            sprite.flipX = false;
            rb.velocity += Vector2.right * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += Vector2.down * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            sprite.flipX = true;
            rb.velocity += Vector2.left * speed;
        }

        //play walking anims
        if (Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) {
                anim.Play("player_walk_upRight");
            } else {
                anim.Play("player_walk_up");
            }
        } else if (Input.GetKey(KeyCode.S)) {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) {
                anim.Play("player_walk_downRight");
            } else {
                anim.Play("player_walk_down");
            }
        } else {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) {
                anim.Play("player_walk_right");
            }
        }

        // if no new inputs, player stops walking and sets standing sprite
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) {
            anim.enabled = false;
            audioSource.Pause();
            if (prevInputs[0]) {
                if (prevInputs[3] || prevInputs[1]) {
                    sprite.sprite = standing[3];
                } else {
                    sprite.sprite = standing[4];
                }
            } else if (prevInputs[2]) {
                if (prevInputs[3] || prevInputs[1]) {
                    sprite.sprite = standing[1];
                } else {
                    sprite.sprite = standing[0];
                }
            } else if (prevInputs[3] || prevInputs[1]) {
                    sprite.sprite = standing[2];
            } else {
                sprite.sprite = standing[0];
            }
        } else {
            prevInputs = new bool[4]{Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.S), Input.GetKey(KeyCode.D)};
            anim.enabled = true;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        
        //Interact With Object
        // if (Input.GetMouseButton(0))
        // {
            Vector2 facing = transform.right;
            if (sprite.flipX == true) facing *= -1;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, facing, interactDis, interactLayer.value);
            if (hit)
            {
                //make tv glow here
                eventsController comp = hit.transform.gameObject.GetComponent<eventsController>();
                Interactable script = hit.transform.gameObject.GetComponent<Interactable>();
                bed Bed = hit.transform.gameObject.GetComponent<bed>();
                if (comp != null && script != null && !script.typing)
                {
                    if (Input.GetKeyDown(KeyCode.E)) //if player raycast touches and player interacts with tv 
                    {
                        //tv turns on;
                        script.TypeWords(comp.returnEvent());
                        //if last event, trigger knock
                    }
                }
                // Interactable script;
                // if ((script = hit.transform.gameObject.GetComponent<Interactable>()) != null && !script.typing)
                // {
                //     script.TypeWords();
                // }
                if (Bed != null) //mimimimi
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Bed.sleep();
                    }
                }
            }
        //}
    }
}
