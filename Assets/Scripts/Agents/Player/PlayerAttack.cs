﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public float maxMagicPool = 5;
    private float magicPool;
    private float attackDelay;//time btwn last attack and next successful attack request
    private float hitDelay; // Time between attack animation start and damage dealt
    public float attackCooldown;//time between successful attack requests
    public float attackRange;//radius of attack circle
    public float damage;
    public bool IsTransformed { get; set; }

    [SerializeField] private Transform attackPosSide, attackPosUp, attackPosDown;
    private Transform currAttackPos = null;
    [SerializeField] private LayerMask enemyLayer, platformLayer;
    private BoxCollider2D m_boxCollider2d;

    private void Start()
    {
        float stat_Magic = PlayerPrefs.GetInt("magic");
        maxMagicPool = stat_Magic;
        magicPool = maxMagicPool;

        if (PlayerPrefs.GetInt("attack") >= 3)
        {
            attackRange += 0.5f;
            anim.SetBool("LongSword", true);
        }

        currAttackPos = attackPosSide;
        m_boxCollider2d = gameObject.GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (hitDelay > 0)
        {
            hitDelay -= Time.deltaTime;

            if (hitDelay <= 0)
            {
                Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(currAttackPos.position, attackRange, enemyLayer);

                for (int i = 0; i < enemiesToDmg.Length; i++)
                {
                    bool right = (enemiesToDmg[i].GetComponent<Rigidbody2D>().position.x > currAttackPos.position.x);
                    enemiesToDmg[i].GetComponent<Health>().takeDamage(damage + PlayerPrefs.GetInt("attack"), right);
                }

                attackDelay = attackCooldown;
            }
        }
        else if(attackDelay <= 0 && IsTransformed == false)
        {
            //You can attack!
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Player Attacks!");

                //determine direction of attack
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    currAttackPos = attackPosUp;
                    anim.SetBool("AttackingUp", true);
                }
                else if (Input.GetKey(KeyCode.DownArrow) && isGrounded() == false)
                {
                    currAttackPos = attackPosDown;
                    anim.SetBool("AttackingDown", true);
                }
                else 
                {
                    currAttackPos = attackPosSide;
                    anim.SetBool("AttackingSide", true);
                }

                hitDelay = 0.125f;
            }
        }
        else
        {
            //attack is on cooldown
            attackDelay -= Time.deltaTime;
            anim.SetBool("AttackingUp", false);
            anim.SetBool("AttackingDown", false);
            anim.SetBool("AttackingSide", false);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(m_boxCollider2d.bounds.center, m_boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformLayer);
        Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }

    void OnDrawGizmosSelected()
    {
        //draws circle around most recent attack position
        //defaults to attackPosSide if currAttackPos uninitialized (no attacks yet)
        Gizmos.color = Color.red;
        if(currAttackPos != null)
        {
            Gizmos.DrawWireSphere(currAttackPos.position, attackRange);
        }

        else
        {
            Gizmos.DrawWireSphere(attackPosSide.position, attackRange);
        }
    }
    public bool isMagicPoolEmpty()
    {
        return magicPool <= 0;
    }
    public void drainMagicPool()
    {
        magicPool--;
    }

    public int getMagicPool()
    {
        return (int)magicPool;
    }

}
