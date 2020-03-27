using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attackDelay;//time btwn last attack and next successful attack request
    public float attackCooldown;//time between successful attack requests
    public float attackRange;//radius of attack circle
    public float damage;

    [SerializeField] private Transform attackPosSide, attackPosUp, attackPosDown;
    private Transform currAttackPos = null;
    [SerializeField] private LayerMask enemyLayer, platformLayer;
    private BoxCollider2D m_boxCollider2d;

    private void Start()
    {
        currAttackPos = attackPosSide;
        m_boxCollider2d = gameObject.GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if(attackDelay <= 0)
        {
            //You can attack!
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Player Attacks!");

                //determine direction of attack
                if (Input.GetKey(KeyCode.UpArrow)) currAttackPos = attackPosUp;
                else if (Input.GetKey(KeyCode.DownArrow) && isGrounded() == false) currAttackPos = attackPosDown;
                else currAttackPos = attackPosSide;

                Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(currAttackPos.position, attackRange, enemyLayer);

                for (int i = 0; i < enemiesToDmg.Length; i++)
                {
                    enemiesToDmg[i].GetComponent<Health>().takeDamage(damage + PlayerPrefs.GetInt("attack"));
                }

                attackDelay = attackCooldown;
            }
        }

        else
        {
            //attack is on cooldown
            attackDelay -= Time.deltaTime;
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

}
