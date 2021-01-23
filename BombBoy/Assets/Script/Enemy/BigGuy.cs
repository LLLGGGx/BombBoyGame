using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BigGuy : Enemy, IDamageable
{
    public Transform PickupPoint;
    public float power = 1;
    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }
    public void PickUpBomb()//anim event
    {
        if (targetPoint.CompareTag("Bomb") && !hasBoom)
        {
            targetPoint.gameObject.transform.position = PickupPoint.position;
            targetPoint.SetParent(PickupPoint);
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            hasBoom = true;
        }
    }
    public void ThrowAway()//anim event
    {
        if (hasBoom)
        {
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            targetPoint.SetParent(transform.parent.parent);

            if (FindObjectOfType<PlayerController>().gameObject.transform.position.x - transform.position.x < 0)
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * power, ForceMode2D.Impulse);
            else
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * power, ForceMode2D.Impulse);
        }
        hasBoom = false;
    }
}
