using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator animator;
    [Header("Setting")]
    public float damageValue = 3;
    public float BombForce;
    public float startTime;
    public float waitTime;
    public float redius;
    public LayerMask targetLayer;
    private void Awake()
    {
        startTime = Time.time;
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BombOff"))
        {
            if (Time.time > startTime + waitTime)
            {
                animator.Play("Explotion");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, redius);
    }
    public void Explotion()
    {
        coll.enabled = false;
        Collider2D[] aroundObject = Physics2D.OverlapCircleAll(transform.position, redius, targetLayer);
        rb.gravityScale = 0;
        foreach (var item in aroundObject)
        {
            Vector3 pos = transform.position - item.transform.position;
            item.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * BombForce, ForceMode2D.Impulse);
            if (item.CompareTag("Bomb")&&item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BombOff"))
            {
                item.GetComponent<Bomb>().TurnOn();
            }
            if (item.CompareTag("Player"))
            { 
                item.GetComponent<IDamageable>().GetHit(damageValue);
            }
        }
    }
    public void DistroyThis()
    {
        Destroy(gameObject);
    }
    public void TurnOff()
    {
        animator.Play("BombOff");
        gameObject.layer = LayerMask.NameToLayer("NPC");
    }
    public void TurnOn()
    {
        startTime = Time.time;
        animator.Play("Bomb");
        gameObject.layer = LayerMask.NameToLayer("Bomb");
    }
}
