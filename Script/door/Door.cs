using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    BoxCollider2D coll;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        coll.enabled = false;
    }
    private void Start()
    {
        GameManager.instance.isDoor(this);
    }
    public void OpenDoor()//Game Manager Takes
    {
        anim.Play("close_open");
        coll.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.NextLevel();
        }
    }
}
