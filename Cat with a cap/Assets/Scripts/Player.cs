using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player obj;

    public int lives = 3;
    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isImmune = false;

    public float speed = 5f;
    public float jumpForce = 6f;
    public float movHor;

    public float immuneTimeCnt = 0f;
    public float immuneTime = 0.5f;

    public LayerMask groundLayer;
    public float radius = 0.2f;
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
        rb = GetComponent<Rigidbody2D>();//Tenemos acceso a todas las propiedades este componente.
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movHor = Input.GetAxisRaw("Horizontal");// Inputs Horizontales del teclado
        
        isMoving = (movHor != 0);

        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) jump();

        flip(movHor); // Cambio de direccion.

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isImmune", isImmune);
    }

    public void jump()
    {
        if (!isGrounded) return;

        rb.velocity = Vector2.up * jumpForce;
    }
    //Todo lo que esté programado funcionará independientemente 
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y); // Permite el movimiento del personaje.
    }

    void flip(float _xValue)
    {
        Vector3 theScale = transform.localScale;

        if (_xValue < 0)
            theScale.x = Mathf.Abs(theScale.x) * -1;
        else if (_xValue > 0)
            theScale.x = Mathf.Abs(theScale.x);

        transform.localScale = theScale;
    }

    public void getDamage()
    {
        lives--;
        if (lives <= 0)
        {
            FXManager.obj.showPop(transform.position);
            Game.obj.gameOver();
        }
    }

    public void addLive()
    {
        lives++;
        if (lives > Game.obj.maxLives)
            lives = Game.obj.maxLives;
    }
    
    void OnDestroy()
    {
        obj = null;    
    }
}
