using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attackDelay;//time btwn last attack and next successful attack request
    public float attackCooldown;//time between successful attack requests

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayer;
    public int damage;
    void Update()
    {
        if(attackDelay <= 0)
        {
            //You can attack!
            if (Input.GetKey(KeyCode.Z))
            {
                Debug.Log("Player Attacks!");
                Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);

                for (int i = 0; i < enemiesToDmg.Length; i++)
                {
                    enemiesToDmg[i].GetComponent<EnemyCombat>().takeDamage(damage);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
