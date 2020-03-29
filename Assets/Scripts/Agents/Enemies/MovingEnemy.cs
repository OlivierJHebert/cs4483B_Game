﻿using System.Collections;
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

    public 

    void Start()
    {
        m_body = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, patrolSpeed * Time.deltaTime);
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

    public void knockback(bool right)
    {
        // Start the knockback timer
        knockbackTimer = 0.3f;

        // Knock the enemy away from the source of damage
        int direction = (right ? 5 : -5);
        m_body.velocity = new Vector2(direction, 10);
    }
}
