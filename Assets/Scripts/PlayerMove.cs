using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float dashStrength = 2f;
    public float dashCooldown = 10f;
    private float timeNextDash = 0.0f;//the timestamp beyond which the player may dash again

    public float leapStrength = 2f;
    private bool leaping = false;//1 when true, 0 when false

    //private Color defaultSpriteColor;
    //public Color leapingSpriteColor;

    private Sprite PlainForm;
    public float p_WalkSpeed, p_JumpSpeed;
    private float p_Grav;
    private PhysicsMaterial2D p_Material;

    public Sprite FlatForm;
    public float f_WalkSpeed, f_JumpSpeed;
    public float f_Grav;
    public PhysicsMaterial2D f_Material;

    public Sprite BallForm;
    public float b_WalkSpeed, b_JumpSpeed;
    public float b_Grav;
    public PhysicsMaterial2D b_Material;

    [SerializeField] private LayerMask platformsLayerMask;

    private Rigidbody2D m_body;
    private BoxCollider2D m_boxCollider2d;
    private SpriteRenderer m_spriteRenderer;
    private float m_WalkSpeed, m_JumpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_body = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        m_boxCollider2d = gameObject.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        m_spriteRenderer = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        //defaultSpriteColor = m_spriteRenderer.color;
        PlainForm = m_spriteRenderer.sprite;
        m_WalkSpeed = p_WalkSpeed;
        m_JumpSpeed = p_JumpSpeed;
        p_Material = m_body.sharedMaterial;
        p_Grav = m_body.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Quitting
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        //jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded())
        {
            m_body.velocity += Vector2.up * (m_JumpSpeed + (leaping ? leapStrength : 0));
            leaping = false;
            m_spriteRenderer.color = new Color(255,255,255);//defaultSpriteColor;
        }

        //walking: speed is magnitude (see velocity assignment), axis is direction
        float h = Input.GetAxisRaw("Horizontal");

        m_body.velocity = new Vector2(h * m_WalkSpeed, m_body.velocity.y);

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

        //changing forms
        if (Input.GetKeyDown(KeyCode.Alpha1) && m_spriteRenderer.sprite != PlainForm)//tap the 1 key to enter PlainForm
        {
            m_spriteRenderer.sprite = PlainForm;
            m_WalkSpeed = p_WalkSpeed;
            m_JumpSpeed = p_JumpSpeed;
            m_body.gravityScale = p_Grav;
            m_body.sharedMaterial = p_Material;
        }

        //floats, moves slowly, short jump
        else if(Input.GetKeyDown(KeyCode.Alpha2) && m_spriteRenderer.sprite != FlatForm)//tap the 2 key to enter FlatForm
        {
            m_spriteRenderer.sprite = FlatForm;
            m_WalkSpeed = f_WalkSpeed;
            m_JumpSpeed = f_JumpSpeed;
            m_body.gravityScale = f_Grav;
            m_body.sharedMaterial = f_Material;
        }

        //bounces, moves quickly
        else if (Input.GetKeyDown(KeyCode.Alpha3) && m_spriteRenderer.sprite != BallForm)//tap the 3 key to enter the BallForm
        {
            m_spriteRenderer.sprite = BallForm;
            m_WalkSpeed = b_WalkSpeed;
            m_JumpSpeed = b_JumpSpeed;
            m_body.gravityScale = b_Grav;
            m_body.sharedMaterial = b_Material;
        }

        //Abilities
        else if (Input.GetKeyDown(KeyCode.X) && !leaping && m_spriteRenderer.sprite != FlatForm)//tap S to enter leap state
        {
            leaping = true;
            m_spriteRenderer.color = new Color(0,0,0);//leapingSpriteColor;
        }

        else if (Input.GetKeyDown(KeyCode.C))//tap S to dash in the direction you're moving
        {
            if (Time.time >= timeNextDash)
            {
                m_body.velocity += new Vector2(h * m_WalkSpeed * dashStrength, 0);
                timeNextDash = Time.time + dashCooldown;
            }
        }

        /* Attack
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //attack, destroy enemy, spawn pulp
        }
        */

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(m_boxCollider2d.bounds.center, m_boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }

}
