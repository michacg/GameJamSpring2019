using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;

    private Animator m_anim;
    private Rigidbody2D m_rigidbody;
    private float movementModifier;
    private Vector2 currentVelocity;
    private Vector2 maxVelocity;

    void Awake()
    {
		m_anim = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
		movementModifier = GetMovementModifier(); 
		Move(movementModifier);
		Animate(movementModifier);
    }

    float GetMovementModifier()
    {
        if ( gameObject.CompareTag("Player1") )
        {
            return Input.GetAxisRaw("Player 1 Horizontal");
        }

        else if ( gameObject.CompareTag("Player2") )
        {
            Debug.Log(Input.GetAxisRaw("Player 2 Horizontal"));
            return Input.GetAxisRaw("Player 2 Horizontal");
        }

        return 0;
    }
    void Move(float mm)
    {

        Vector2 newVelocity = m_rigidbody.velocity;
        newVelocity.x = mm * speed;
       	m_rigidbody.velocity = newVelocity;
    }

    void Animate(float mm)
    {
    	currentVelocity = m_rigidbody.velocity;
    	//maxVelocity     = GetMaxVelocity();

    	if (mm == 1)
    	{
    		//m_anim.SetTrigger("GoRight");
    	}
    	
    	if (mm == -1)
    	{
    		//m_anim.SetTrigger("GoLeft");
    	}

    	/*
    	if (currentVelocity > 0)
    	{
    		//m_anim.SetTrigger("Accelerate");
    	}
		*/

    	/*
    	if (currentVelocity == maxVelocity)
    	{
    		//m_anim.SetTrigger("MaxVelocity");
    	}
    	*/

    }

    /*
    Vector2 GetMaxVelocity()
    {
    	
    }
	*/
}