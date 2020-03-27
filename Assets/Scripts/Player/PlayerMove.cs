using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //jumping parameters
    public float maxJumpTime;//the maximum time for which holding down 'jump' increases jump height
    private float jumpTimeCounter;
    private bool isJumping;
    public float fallFactor;//the number by which gravity is multiplied when falling

    //dash ability parameters
    public float dashStrength = 2f;
    public float dashCooldown = 10f;
    private float timeNextDash = 0.0f;//the timestamp beyond which the player may dash again

    //leap ability parameters
    public float leapStrength = 2f;
    private bool leaping = false;//1 when true, 0 when false

    //player shapeshift state parameters
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
        m_body = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        m_boxCollider2d = gameObject.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        m_spriteRenderer = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        //init player shapeshift state
        shapeshift(plainForm);

        //init jump parameters
        jumpTimeCounter = 0;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        /**********  Quitting  **********/
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        /**********  Jumping  **********/
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            m_body.velocity = Vector2.up * (currentForm.JumpStrength + (leaping ? leapStrength : 0));
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            m_spriteRenderer.color = new Color(255,255,255);//defaultSpriteColor;
        }
        
        //if the jump key is held, the player continues to jump for a short time
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                m_body.velocity = Vector2.up * (currentForm.JumpStrength + (leaping ? leapStrength : 0));
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
        
        if(m_body.velocity.y < 0)//increase gravity during fall
        {
            m_body.velocity += Vector2.up * Physics2D.gravity.y * (fallFactor - 1) * Time.deltaTime;
        }

        /**********  Walking  **********/
        //speed is magnitude (see velocity assignment), axis is direction
        //Input.GetAxis(...) for smooth movement, Input.GetAxisRaw(...) for snappy movement
        float walkInput = Input.GetAxis("Horizontal");

        m_body.velocity = new Vector2(walkInput * (currentForm.WalkSpeed + PlayerPrefs.GetInt("speed")), m_body.velocity.y);
        
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
        else if(Input.GetKeyDown(KeyCode.Alpha2) && currentForm != flatForm)
        {
            shapeshift(flatForm);
        }

        //tap the 3 key to enter the BallForm
        //bounces, moves quickly
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currentForm != ballForm)
        {
            shapeshift(ballForm);
        }

        /**********  Abilities  **********/
        else if (Input.GetKeyDown(KeyCode.X) && !leaping && currentForm == flatForm)//tap S to enter leap state
        {
            leaping = true;
            m_spriteRenderer.color = new Color(0,0,0);//leapingSpriteColor;
        }

        else if (Input.GetKeyDown(KeyCode.C))//tap S to dash in the direction you're moving
        {
            if (Time.time >= timeNextDash)
            {
                m_body.velocity += new Vector2(walkInput * currentForm.WalkSpeed * dashStrength, 0);
                timeNextDash = Time.time + dashCooldown;
            }
        }

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(m_boxCollider2d.bounds.center, m_boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }

    private void shapeshift(Form next)
    {
        currentForm = next;
        m_body.gravityScale = next.GravityScale;
        m_body.sharedMaterial = next.Material;
        m_spriteRenderer.sprite = next.Sprite;
    }

}
