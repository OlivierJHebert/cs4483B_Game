using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : PlayerHazardCollider, IMove
{
    public float patrolSpeed;
    public float distance;

    private float knockbackTimer = 0;

    [SerializeField] private LayerMask platformsLayerMask;
    public Transform groundDetection;

    private bool m_MovingRight = true;

    private Rigidbody2D m_body;

    void Start()
    {
        m_body = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (knockbackTimer > 0)
            knockbackTimer -= Time.deltaTime;
        else
            transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, platformsLayerMask);

        if(groundInfo.collider == false)
        {
            if(m_MovingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                m_MovingRight = false;
            }

            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                m_MovingRight = true;
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject == player && !player.GetComponent<PlayerMove>().invincible())
        {
            bool right = (player.GetComponent<Rigidbody2D>().position.x > m_body.position.x);
            player.GetComponent<Health>().takeDamage(damage, right);
        }
    }

    public void TriggerWaterEffect(float time)
    {
        //slow the enemy (TO-DO)
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
