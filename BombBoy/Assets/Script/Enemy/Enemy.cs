using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyBaseState currentState;
    public Animator anim;
    public int animState;
    private GameObject alarmSign;
    [Header("Base state")]
    public float health = 5;
    public bool isDead;
    public bool hasBoom;
    public bool isBoss;
    [Header("Movement")]
    public float speed;
    public Transform pointA, PointB;
    public Transform targetPoint;
    [Header("Attack Setting")]
    public float attackRate;
    private float nextAttack = 0;
    public float attackRange, skillRange;
    public List<Transform> attackList = new List<Transform>();
    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();
    public virtual void Init()
    {
        anim = GetComponent<Animator>();
        alarmSign = transform.GetChild(0).gameObject;
    }
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        GameManager.instance.IsEnemy(this);
        TransitionToState(patrolState); 
        if (isBoss)
            UIManager.instance.SetBossHealth(health);     
    }
    public virtual void Update()
    {
        anim.SetBool("dead", isDead);
        if (isDead)
        {   
            GameManager.instance.Enemydead(this);
            return;
        }
        currentState.OnUpdata(this);
        anim.SetInteger("state", animState);

        if(isBoss){
            UIManager.instance.UpdateBossHealth(health);
        }
    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FilpDirection();
    }
    public virtual void AttackAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {
                //play attack anim
                anim.SetTrigger("attack");
                nextAttack = Time.time + attackRate;
                Debug.Log("攻击");
            }
        }
    }
    public virtual void SkillAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextAttack)
            {
                //play skill anim
                anim.SetTrigger("skill");
                nextAttack = Time.time + attackRate;
                Debug.Log("技能");
            }
        }
    }
    public void FilpDirection()
    {
        if (transform.position.x < targetPoint.position.x)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
    public void SwitchPoint()
    {
        if (Mathf.Abs(pointA.position.x - transform.position.x) > Mathf.Abs(PointB.position.x - transform.position.x))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = PointB;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!attackList.Contains(other.transform) && !hasBoom && !GameManager.instance.gameOver)
            attackList.Add(other.transform);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        attackList.Remove(other.transform);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead && !GameManager.instance.gameOver)
            StartCoroutine("OnAlarm");
    }
    IEnumerator OnAlarm()
    {
        alarmSign.SetActive(true);
        yield return new WaitForSeconds(alarmSign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        alarmSign.SetActive(false);
    }
}
