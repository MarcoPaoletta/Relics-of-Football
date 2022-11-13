using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public LayerMask groundMask;
    public GameObject walkDust;
    public GameObject jumpDust;
    public int speed = 10;
    public int jumpForce = 15;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    MovementButtonsListener leftMovementButton;
    MovementButtonsListener rightMovementButton;
    MovementButtonsListener upMovementButton;
    RaycastHit2D rayCast;
    bool isCoroutineAllowed;
    bool grounded;
    bool isJetpackOn = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        leftMovementButton = GameObject.FindGameObjectWithTag("LeftMovementButton").GetComponent<MovementButtonsListener>();
        rightMovementButton = GameObject.FindGameObjectWithTag("RightMovementButton").GetComponent<MovementButtonsListener>();
        upMovementButton = GameObject.FindGameObjectWithTag("UpMovementButton").GetComponent<MovementButtonsListener>();
    }

    void Update()
    {
        Movement();
        SpriteRotation();
        SetAnimations();
        StartSpawnDustCoroutine();
        TemporalFeltDetection();
    }

    void Movement()
    {
        if(!isJetpackOn)
        {

            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
    
            if(grounded)
            {
                if(Input.GetKey(KeyCode.W) || upMovementButton.isButtonHeldDown)
                {
                    Jump();
                }
            }

            if(leftMovementButton.isButtonHeldDown)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }

            if(rightMovementButton.isButtonHeldDown)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetTrigger("jump");
    }

    void SpriteRotation()
    {
        if(rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if(rb.velocity.x > 0 || rb.velocity == Vector2.zero)
        {
            spriteRenderer.flipX = false;
        }
    }

    void SetAnimations()
    {
        animator.SetBool("walking", Input.GetAxisRaw("Horizontal") != 0);
        animator.SetBool("grounded", grounded);
    }

    void StartSpawnDustCoroutine()
    {
        if(grounded && isCoroutineAllowed && rb.velocity.x != 0)
        {
            StartCoroutine("SpawnDust");
            isCoroutineAllowed = false;
        }
        if(rb.velocity.x == 0 || !grounded)
        {
            StopCoroutine("SpawnDust");
            isCoroutineAllowed = true;
        }
    }

    void TemporalFeltDetection()
    {
        if(transform.position.y < -6)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator SpawnDust()
    {
        while(grounded)
        {
            Vector3 dustPosition = new Vector3(transform.position.x, transform.position.y - 0.65f, transform.position.z);
            Instantiate(walkDust, dustPosition, walkDust.transform.rotation);
                yield return new WaitForSeconds(0.1f);
        }   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Vector3 dustPosition = new Vector3(transform.position.x, transform.position.y - 0.65f, transform.position.z);
            isCoroutineAllowed = true;
            Instantiate(walkDust, dustPosition, walkDust.transform.rotation);
            grounded = true;
        }            
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isCoroutineAllowed = false;
            grounded = false;
        }
    }

    public void Jetpack()
    {
        rb.velocity = new Vector2(jumpForce * 1.5f, jumpForce * 2);
        animator.SetTrigger("jump");
        isJetpackOn = true;
        Invoke("SetJetpackOff", 1);
    }

    void SetJetpackOff()
    {
        isJetpackOn = false;
    }
}
