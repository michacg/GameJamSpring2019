using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float terminalVelocity = -10;
    public float rayLen = 1f;
    public int coolDownSec = 4;
    public PlayerController otherPlayer;
    public GameObject leftUI;
    public GameObject rightUI;
    public GameObject breakUI;

    public Sprite l_normal;
    public Sprite r_normal;
    public Sprite l_down;
    public Sprite r_down;

    private enum PlayerType {first, second};

    private SpriteRenderer m_SpriteRenderer;

    private Image l_image;
    private Image r_image;
    private Image b_image;

    private Animator m_anim;
    private Rigidbody2D m_rigidbody;
    private float movementModifier;
    private PlayerType pType;
    private bool canDestroy;
    private bool justLanded;
    private bool isCooldown;

    private float cooldownCount;

    void Awake()
    {
        l_image = leftUI.GetComponent<Image>();
        r_image = rightUI.GetComponent<Image>();
        b_image = breakUI.GetComponent<Image>();

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
		m_anim = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();
        pType = getPlayerType();
        canDestroy = true;
        isCooldown = false;
        cooldownCount = 0f;
    }

    void Update()
    {
		movementModifier = GetMovementModifier();

        if (movementModifier == 1)
        {
            r_image.sprite = r_down;
            l_image.sprite = l_normal;
        }

        else if (movementModifier == -1)
        {
            l_image.sprite = l_down;
            r_image.sprite = r_normal;
        }

        else
        {
            r_image.sprite = r_normal;
            l_image.sprite = l_normal;
        }
        
		Animate(movementModifier);
        Move(movementModifier);

        if (canDestroy && (Input.GetKeyDown(KeyCode.S) && pType == PlayerType.first || Input.GetKeyDown("down") && pType == PlayerType.second))
        {
            DestroyOtherPlatform();
        }

        if (isCooldown)
        {
            cooldownCount += Time.deltaTime/60;
            b_image.fillAmount += (cooldownCount / coolDownSec);
        }

        else
        {
            cooldownCount = 0;
            b_image.fillAmount = 1;
            isCooldown = false;
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
            otherPlayer.destroyOwnPlatform();
        }

        else
        {
                canDestroy = false;
                isCooldown = true;
                StartCoroutine("CoolDown");
        }

    }

    IEnumerator CoolDown()
    {
        b_image.fillAmount = 0;
        
        for (float i = 0; i >= coolDownSec; i += Time.deltaTime)
        {
        }
        yield return new WaitForSeconds(coolDownSec);
        canDestroy = true;
    }

    void Animate(float mm)
    {

        //ON PLATFORM
        if (checkOnPlatform())
        {
            if (justLanded)
            {
                m_anim.SetTrigger("TriggerLand");
                justLanded = false;
            }
        }

        //FALLING
    	else 
        {
            justLanded = true;
            if (mm == 0)
            {
                if (atMaxVelocity())
                {
                    m_anim.SetTrigger("TriggerFallTerminal");
                }

                else
                {
                    m_anim.SetTrigger("TriggerFallAccel");
                }
            }

            else if (mm == 1)
            {
                if (atMaxVelocity())
                {
                    m_anim.SetTrigger("TriggerRLFallTerminal");
                }

                else
                {
                    m_anim.SetTrigger("TriggerRLFallAccel");
                }

                m_SpriteRenderer.flipX = false;

            }

            else if (mm == -1) 
            {
                if (atMaxVelocity())
                {   
                    m_anim.SetTrigger("TriggerRLFallTerminal");
                }

                else
                {
                    m_anim.SetTrigger("TriggerRLFallAccel");
                }

                m_SpriteRenderer.flipX = true;
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

    bool checkOnPlatform()
    {
        int layerMask;
        if (pType == PlayerType.first)
            layerMask = LayerMask.GetMask("Platform 1");
        else
            layerMask = LayerMask.GetMask("Platform 2");

        Vector2 bottomPosition = new Vector2(this.transform.position.x, GetComponent<Collider2D>().bounds.min.y);

        bool isOnPlatform = Physics2D.Raycast(bottomPosition, Vector2.down, .1f, layerMask);

        return isOnPlatform;
        
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