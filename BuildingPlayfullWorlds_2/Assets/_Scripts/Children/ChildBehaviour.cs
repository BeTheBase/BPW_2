using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBehaviour : MonoBehaviour
{
    public Transform Player;
    public float DistanceToPlayer = 4f;

    private bool followPlayer = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (followPlayer)
            FollowThePlayer();
    }

    private void FollowThePlayer()
    {
 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
            followPlayer = true;
            animator.SetBool("ChildFollow", true);
            if (!GameManager.Children.ContainsKey(gameObject.name))
            {
                GameManager.Children.Add(gameObject.name, gameObject);
                GameManager.CheckChildren();
            }
            else
                return;
        }
    }
}
