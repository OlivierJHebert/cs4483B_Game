using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : PlayerHazardCollider, IMove
{
    public float patrolSpeed;
    public float distance;

    private float knockbackTimer = 0;

    [SerializeField] private LayerMask platformsLayerMask;
    public Transform groundDetection;
    private Rigidbody2D m_body;
    private bool isJumping = false;
    private float jumpTimeCounter = 0;
    public float currJumpStrength = 21f;
    public float maxJumpTime = 0.17f;
    public float fallFactor = 2;

    void Start()
    {
        m_body = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {


        if (player.transform.position.y - 5 > m_body.position.y)
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            if (jumpTimeCounter > 0 && isJumping)
            {
                m_body.velocity = new Vector2(m_body.velocity.x, currJumpStrength);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (m_body.velocity.y < 0)//increase gravity during fall
        {
            m_body.velocity += Vector2.up * Physics2D.gravity.y * (fallFactor - 1) * Time.deltaTime;
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, patrolSpeed * Time.deltaTime);

        // Loop the top and bottom sides of the stage
        if (transform.position.y > 11) // Top
            transform.position = new Vector2(transform.position.x, -11);
        else if (m_body.position.y < -11) // Bottom
            transform.position = new Vector2(transform.position.x, 11);

        // Loop the left and right sides of the stage
        if (transform.position.x < -20) // Left
            transform.position = new Vector2(18, transform.position.y);
        else if (transform.position.x > 18) // Right
            transform.position = new Vector2(-20, transform.position.y);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player && !player.GetComponent<PlayerMove>().invincible())
        {
            bool right = (player.GetComponent<Rigidbody2D>().position.x > m_body.position.x);
            player.GetComponent<Health>().takeDamage(damage, right);
        }
    }

    public void TriggerWaterEffect(float time)
    {
        Debug.Log("Trigger Water Effect!");
    }

    public void knockback(bool right, bool damaged)
    {
        // Start the knockback timer
        knockbackTimer = 0.3f;

        // Knock the enemy away from the source of damage
        int direction = (right ? 5 : -5);
        m_body.velocity = new Vector2(direction, 10);
    }
}
