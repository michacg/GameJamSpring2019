﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 7;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(0, -speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            Debug.Log("Player 1 WOOOOOOOOOOOOOOOOOOOOOOOOON yeet");
        }
        else if (collision.gameObject.name == "Player 2")
        {
            Debug.Log("Player 2 is winner wooohoooooo");
        }
    }
}
