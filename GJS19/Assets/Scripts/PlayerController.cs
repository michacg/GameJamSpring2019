using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float terminalVelocity = -10;
    public bool isNearPlatform;

    private Animator m_anim;
    private Rigidbody2D m_rigidbody;
    private float movementModifier;

    void Awake()
    {
		m_anim = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();
        isNearPlatform = false;
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
        if ( gameObject.CompareTag("Player1") )
        {
            return Input.GetAxisRaw("Player 1 Horizontal");
        }

        else if ( gameObject.CompareTag("Player2") )
        {
            return Input.GetAxisRaw("Player 2 Horizontal");
        }

        return 0;
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
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        Vector2 rayCastPosition = transform.position;
        rayCastPosition.y += .5f;
        isNearPlatform = Physics2D.Raycast(rayCastPosition, transform.TransformDirection(Vector3.down), 1f, layerMask);
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1f, Color.yellow);

        // if(isNearPlatform)
        //    Debug.Log("TRUE");
    }

    bool atMaxVelocity()
    {
        return (m_rigidbody.velocity.y <= terminalVelocity);
    }

}