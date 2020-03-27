using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMove
{
    //wetTrigger: slows player
    private float slowedTimer = 0;

    // Knockback parameters
    private float knockbackTimer = 0;
    private float alpha = 1.0f;
    
    //jumping parameters
    public float maxJumpTime;//the maximum time for which holding down 'jump' increases jump height
    private float jumpTimeCounter = 0;
    private bool isJumping = false;//signals if the player is in the middle of an initiated jump
    private bool doubleJumping = false;//signals whether the player has jumped midair
    public float fallFactor;//the number by which gravity is multiplied when falling

    //dash ability parameters
    public float dashStrength = 2f;
    public float dashCooldown = 10f;
    private float timeNextDash = 0.0f;//the timestamp beyond which the player may dash again

    //leap ability parameters
    public float leapStrength = 2f;
    private bool leaping = false;//1 when true, 0 when false

    //player shapeshift state parameters
    private PlayerAttack attackScript;
    private Form currentForm;
    [SerializeField] private Form plainForm;
    [SerializeField] private Form ballForm;
    [SerializeField] private Form flatForm;

    [SerializeField] private LayerMask platformsLayerMask;

    private Rigidbody2D m_body;
    private BoxCollider2D m_boxCollider2d;
    private SpriteRenderer m_spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //init component references
        m_body = gameObject.GetComponent<Rigidbody2D>();
        m_boxCollider2d = gameObject.GetComponent<BoxCollider2D>();
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        attackScript = gameObject.GetComponent<PlayerAttack>();

        //init player shapeshift state
        shapeshift(plainForm);
    }

    // Update is called once per frame
    void Update()
    {
        
        /**********  Quitting  **********/
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        /**********  Water Trigger: Slowed  **********/
        float currJumpStrength = currentForm.JumpStrength;
        float currWalkSpeed = currentForm.WalkSpeed + PlayerPrefs.GetInt("speed");

        if (slowedTimer > 0)
        {
            slowedTimer -= Time.deltaTime;
            currJumpStrength *= 0.5f;
            currWalkSpeed *= 0.5f;
        }

        /**********  Knockback  **********/
        // When the knockback timer is active, the player is unable to move for a short time (0.3 seconds)
        // During this time, the velocity will be changed to send the player away from the damaging object
        // For the rest of the timer duration, the player will be unable to take damage
        // This allows a brief period to react and ensures the player can't be unfairly killed instantly
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;

            // Flash player sprite to indicate invincibility
            alpha = ((alpha == 1) ? 0 : 1);
        }
        else
        {
            alpha = 1;
        }

        Color color = m_spriteRenderer.color;
        color.a = alpha;
        m_spriteRenderer.color = color;

        /**********  Jumping  **********/
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded() || (doubleJumping == false && PlayerPrefs.GetInt("speed") >= 3)))
        {
            m_body.velocity = Vector2.up * (currJumpStrength + (leaping ? leapStrength : 0));
            if (!isGrounded()) doubleJumping = true;
            else
            {
                isJumping = true;
                doubleJumping = false;
            }
            jumpTimeCounter = maxJumpTime;
            m_spriteRenderer.color = new Color(255,255,255,alpha);//defaultSpriteColor;
        }
        
        //if the jump key is held, the player continues to jump for a short time
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                m_body.velocity = Vector2.up * (currJumpStrength + (leaping ? leapStrength : 0));
                jumpTimeCounter -= Time.deltaTime;
            }

            else
            {
                isJumping = false;
                leaping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            leaping = false;
        }
        
        if(m_body.velocity.y < 0 && currentForm == plainForm)//increase gravity during fall
        {
            m_body.velocity += Vector2.up * Physics2D.gravity.y * (fallFactor - 1) * Time.deltaTime;
        }

        /**********  Walking  **********/
        //speed is magnitude (see velocity assignment), axis is direction
        //Input.GetAxis(...) for smooth movement, Input.GetAxisRaw(...) for snappy movement
        float walkInput = Input.GetAxis("Horizontal");

        if (knockbackTimer < 0.5f)
            m_body.velocity = new Vector2(walkInput * currWalkSpeed, m_body.velocity.y);

        //player facing (flips according to horizontal input)
        if(walkInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else if (walkInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Loop the top and bottom sides of the stage
        if (m_body.position.y > 11) // Top
            m_body.position = new Vector2(m_body.position.x, -11);
        else if (m_body.position.y < -11) // Bottom
            m_body.position = new Vector2(m_body.position.x, 11);

        // Loop the left and right sides of the stage
        if (m_body.position.x < -20) // Left
            m_body.position = new Vector2(18, m_body.position.y);
        else if (m_body.position.x > 18) // Right
            m_body.position = new Vector2(-20, m_body.position.y);

        /**********  Changing Forms  **********/
        //tap the 1 key to enter PlainForm
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentForm != plainForm)
        {
            shapeshift(plainForm);
        }
        
        //tap the 2 key to enter FlatForm
        //floats, moves slowly, short jump
        else if(Input.GetKeyDown(KeyCode.Alpha2) && PlayerPrefs.GetInt("magic") >= 3 && attackScript.isMagicPoolEmpty() == false && currentForm != flatForm)
        {
            attackScript.drainMagicPool();
            shapeshift(flatForm);
        }

        //tap the 3 key to enter the BallForm
        //bounces, moves quickly
        else if (Input.GetKeyDown(KeyCode.Alpha3) && PlayerPrefs.GetInt("magic") >= 5 && attackScript.isMagicPoolEmpty() == false && currentForm != ballForm)
        {
            attackScript.drainMagicPool();
            shapeshift(ballForm);
        }

        /**********  Abilities  **********/
        //remove leaping??
        else if (Input.GetKeyDown(KeyCode.X) && !leaping && currentForm != flatForm)//tap S to enter leap state
        {
            leaping = true;
            m_spriteRenderer.color = new Color(0,0,0,alpha);//leapingSpriteColor;
        }

        else if (Input.GetKeyDown(KeyCode.C))//tap S to dash in the direction you're moving
        {
            if (Time.time >= timeNextDash && isGrounded() && PlayerPrefs.GetInt("speed") >= 5)
            {
                m_body.velocity += new Vector2(walkInput * currentForm.WalkSpeed * dashStrength, 0);
                timeNextDash = Time.time + dashCooldown;
            }
        }

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(m_boxCollider2d.bounds.center, m_boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        //Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }

    private void shapeshift(Form next)
    {
        currentForm = next;
        m_body.gravityScale = next.GravityScale;
        m_body.sharedMaterial = next.Material;
        m_spriteRenderer.sprite = next.Sprite;
    }

    public void TriggerWaterEffect(float time)
    {
        //slow the player
        Debug.Log("Trigger Water Effect!");
        slowedTimer += time;
    }

    public void knockback(bool right)
    {
        // Start the knockback timer
        knockbackTimer = 0.8f;

        // Knock the player away from the source of damage
        int direction = (right ? 5 : -5);
        m_body.velocity = new Vector2(direction, 10);
    }

    public bool invincible()
    {
        return (knockbackTimer > 0);
    }

    public bool facingRight()
    {
        return (transform.eulerAngles.y == 180);
    }
}
