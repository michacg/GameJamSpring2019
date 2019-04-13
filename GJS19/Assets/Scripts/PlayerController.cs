﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float terminalVelocity = -10;
    public bool isNearPlatform;

    private enum PlayerType {first, second};

    private Animator m_anim;
    private Rigidbody2D m_rigidbody;
    private float movementModifier;
    private PlayerType pType;

    void Awake()
    {
		m_anim = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();
        isNearPlatform = false;
        pType = getPlayerType();
        Debug.Log(pType);
    }

    void Update()
    {
		movementModifier = GetMovementModifier(); 
		Move(movementModifier);
		Animate(movementModifier);

        checkNearPlatform();
        DestroyPlatform();
    }

    float GetMovementModifier()
    {
        if (pType == PlayerType.first)
        {
            return Input.GetAxisRaw("Player 1 Horizontal");
        }

        else
            return Input.GetAxisRaw("Player 2 Horizontal");
    }

    void Move(float mm)
    {
        Vector2 newVelocity = m_rigidbody.velocity;
        newVelocity.x = mm * speed;

        if (atMaxVelocity())
            newVelocity.y = terminalVelocity;

       	m_rigidbody.velocity = newVelocity;
    }

    void DestroyPlatform()
    {
        if (Input.GetKey("down"))
            Debug.Log("destroy player 2 platform!");

        if (Input.GetKey(KeyCode.S))
            Debug.Log("destroy player 1 platform!");
    }

    void Animate(float mm)
    {

    	if (mm == 1)
    		//m_anim.SetTrigger("GoRight");
    	
    	if (mm == -1)
    		//m_anim.SetTrigger("GoLeft");

        if (atMaxVelocity())
        {
            //m_anim.SetTrigger("MaxVelocity");
        }

    }

    void checkNearPlatform()
    {
        int layerMask = LayerMask.GetMask("Platform");

        Vector2 bottomPosition = new Vector2(this.transform.position.x, GetComponent<Collider2D>().bounds.min.y);
        isNearPlatform = Physics2D.Raycast(bottomPosition, Vector2.down, 1f, layerMask);
        Debug.DrawRay(bottomPosition, Vector2.down * 1f, Color.yellow);
    }

    bool atMaxVelocity()
    {
        return (m_rigidbody.velocity.y <= terminalVelocity);
    }

    PlayerType getPlayerType()
    {
        if ( gameObject.CompareTag("Player1") )
            return PlayerType.first;

        else
            return PlayerType.second;

    }

}