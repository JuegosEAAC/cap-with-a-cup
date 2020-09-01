using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player obj;

    public int lives = 5;
    public bool isGrounded = false;
    public bool isMoving = false;
    //Posiblemente inmunidad.

    public float speed = 5f;
    public float jumpForce = 6f;
    public float movHor;
    private float decelerate = 0.5f;
    public LayerMask groundLayer;
    public float radius = 0.63f;
    public float groundRayDist = 0.3f;

    //Variables para controlar los componentes del personaje:

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    void Awake()
    {
        obj = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y); // Permite el movimiento del personaje.
    }

    // Update is called once per frame
    void Update()
    {
        movHor = Input.GetAxisRaw("Horizontal");
        
        isMoving = (movHor != 0);

        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) jump();
    }

    public void jump()
    {
        
        if (!isGrounded) return;

        rb.velocity = Vector2.up * jumpForce;
        //rb.velocity = Vector2.down * decelerate;

    }

    public void getDamage()
    {
        lives--;
        if (lives <= 0)
        {
            gameObject.SetActive(false);
        }
    }


    void OnDestroy()
    {
        obj = null;    
    }
}
