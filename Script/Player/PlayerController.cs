using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Animator anim;
    public float jumpForce;
    private bool isHurt;
    public float speed = 0;
    private Rigidbody2D rb;
    private FixedJoystick joystick;
    [Header("Palyer state")]
    public float health;
    public bool isDead;
    [Header("FX")]
    public GameObject jumpFX;
    public GameObject landFX;
    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    [Header("State Check")]
    public bool isGround;
    public bool canJump;
    public bool isJump;
    [Header("Attact Setting")]
    public GameObject bombPrefab;
    public float nextAttack = 0;
    public float attackRate;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        health = GameManager.instance.LoadHealth();
        UIManager.instance.UpdataHealth(health);
        GameManager.instance.isPlayer(this);
        joystick = FindObjectOfType<FixedJoystick>();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("dead", isDead);
        if (isDead)
            return;

        isHurt = anim.GetCurrentAnimatorStateInfo(1).IsName("hit");
        InputCheck();
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (!isHurt)
        {
            Move();
            Jump();
        }
        PhysicsCheck();
    }

    private void InputCheck()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            canJump = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }
    private void Move()
    {   
        //float horizontalInput = Input.GetAxisRaw("Horizontal");
        float horizontalInput = joystick.Horizontal;
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        // if (horizontalInput != 0)
        // {
        //     transform.localScale = new Vector3(horizontalInput, transform.localScale.y, transform.localScale.z);
        // }
        if (horizontalInput > 0)
            transform.eulerAngles = Vector3.zero;
        if ((horizontalInput < 0))
            transform.eulerAngles = new Vector3(0,180,0);
    }
    private void Jump()
    {
        if (canJump)
        {
            jumpFX.SetActive(true);
            jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }
    }

    public void ButtonJump()
    {
        canJump = true;
    }
    public void Attack()
    {
        if (Time.time > nextAttack)
        {
            Instantiate(bombPrefab, transform.position + new Vector3(Random.Range(-0.1f, 0.1f), 0, 0), bombPrefab.transform.rotation);
            nextAttack = Time.time + attackRate;
        }
    }
    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (isGround)
        {
            rb.gravityScale = 1;
            canJump = false;
            isJump = false;
        }
        else
        {
            rb.gravityScale = 4;
            isJump = true;
        }
    }
    public void LandFX()
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    public void GetHit(float damage)
    {
        if (!anim.GetAnimatorTransitionInfo(1).IsName("hit"))
        {
            health -= damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            anim.SetTrigger("hit");

            UIManager.instance.UpdataHealth(health);
        }
    }
}
