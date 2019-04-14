﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float terminalVelocity = -10;
    public float rayLen = 1f;
    public int coolDownSec = 4;
    public PlayerController otherPlayer;

    private enum PlayerType {first, second};

    private SpriteRenderer m_SpriteRenderer;
    private Animator m_anim;
    private Rigidbody2D m_rigidbody;
    private float movementModifier;
    private PlayerType pType;
    private bool canDestroy;
    private bool onPlatform;

    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
		m_anim = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();
        pType = getPlayerType();
        canDestroy = true;
    }

    void Update()
    {
		movementModifier = GetMovementModifier(); 
        //Debug.Log(canDestroyOwnPlatform());
		Move(movementModifier);
		Animate(movementModifier);

        if (canDestroy && (Input.GetKeyDown(KeyCode.S) && pType == PlayerType.first || Input.GetKeyDown("down") && pType == PlayerType.second))
        {
            DestroyOtherPlatform();
        }
    }


    float GetMovementModifier()
    {
        if (pType == PlayerType.first)
            return Input.GetAxisRaw("Player 1 Horizontal");

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

    void DestroyOtherPlatform()
    {
        if (otherPlayer.canDestroyOwnPlatform())
        {
            Debug.Log("can destroy!");
            otherPlayer.destroyOwnPlatform();
        }

        else
        {
                canDestroy = false;
                StartCoroutine("CoolDown");
        }

    }

    IEnumerator CoolDown()
    {
        for (float i = 0; i >= coolDownSec; i += Time.deltaTime)
        {
        
        }
        yield return new WaitForSeconds(coolDownSec);
        canDestroy = true;
    }

    void Animate(float mm)
    {

        //ON PLATFORM
        if (canDestroyOwnPlatform())
        {
            if (mm == 1)
            {
                m_anim.SetTrigger("TriggerRLRoll");
                m_SpriteRenderer.flipX = false;
            }

            else if (mm == -1)
            {
                m_anim.SetTrigger("TriggerRLRoll");
                m_SpriteRenderer.flipX = true;
            }
        }


        //FALLING
    	else 
        {

            if (mm == 1)
            {
                if (atMaxVelocity())
                    m_anim.SetTrigger("TriggerRLFallAccel");

                else
                    m_anim.SetTrigger("TriggerRLFallTerminal");

                m_SpriteRenderer.flipX = false;

            }

            else if (mm == -1) 
            {
                if (atMaxVelocity())
                    m_anim.SetTrigger("TriggerRLFallAccel");

                else
                    m_anim.SetTrigger("TriggerRLFallTerminal");

                m_SpriteRenderer.flipX = true;
            }

            else
            {
                if (atMaxVelocity())
                    m_anim.SetTrigger("TriggerFallAccel");

                else
                    m_anim.SetTrigger("TriggerFallTerminal");

                m_SpriteRenderer.flipX = false;
            }
        }

    }

    bool canDestroyOwnPlatform()
    {
        int layerMask;
        if (pType == PlayerType.first)
            layerMask = LayerMask.GetMask("Platform 1");
        else
            layerMask = LayerMask.GetMask("Platform 2");

        Vector2 bottomPosition = new Vector2(this.transform.position.x, GetComponent<Collider2D>().bounds.min.y);

        bool isNearPlatform = Physics2D.Raycast(bottomPosition, Vector2.down, rayLen, layerMask);
        Debug.DrawRay(bottomPosition, Vector2.down * rayLen, Color.yellow);

        return isNearPlatform;
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }

    void destroyOwnPlatform()
    {
        int layerMask;
        if (pType == PlayerType.first)
            layerMask = LayerMask.GetMask("Platform 1");
        else
            layerMask = LayerMask.GetMask("Platform 2");

        Vector2 bottomPosition = new Vector2(this.transform.position.x, GetComponent<Collider2D>().bounds.min.y);

        RaycastHit2D hit    = Physics2D.Raycast(bottomPosition, Vector2.down, rayLen, layerMask);
        Debug.DrawRay(bottomPosition, Vector2.down * rayLen, Color.yellow);

        Destroy(hit.collider.gameObject);
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