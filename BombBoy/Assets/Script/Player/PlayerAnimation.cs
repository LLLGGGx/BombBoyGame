using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerController controllerScript;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controllerScript = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("jump",controllerScript.isJump);
        animator.SetFloat("velocityY",rb.velocity.y);
        animator.SetBool("ground",controllerScript.isGround);
    }
}
