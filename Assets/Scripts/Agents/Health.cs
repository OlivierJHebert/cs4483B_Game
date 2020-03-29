using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //death manager reference
    public float maxHealth = 4;
    private float health;
    public float fireResist = 2;
    public float waterResist = -2;
    private float statusBuildup = 0;

    public float burnDuration = 3;
    private float burnTimer = 0;
    public float damageInterval = 3;
    private float damageTimer = 0;

    private IMove moveController;
    protected virtual void Start()
    {
        health = maxHealth;
        moveController = gameObject.GetComponent<IMove>();
    }

    protected virtual void Update()
    {
        //Debug.Log("health: " + health.ToString());
        if (health <= 0)
        {
            Kill();
        }

        else if (burnTimer > 0)
        {
            Debug.Log("Agent is burning!");

            burnTimer -= Time.deltaTime;
            damageTimer -= Time.deltaTime;

            if (damageTimer <= 0)
            {
                takeDamage(1, true, 0);
                damageTimer = damageInterval;
            }
        }
    }

    public virtual void Kill()
    {
        Destroy(gameObject);//expand later with death animation
    }

    /* applies damage to the agent, updates status bars according to type of damage applied
     * damageDefinition:   -1 - water damage (slows player)
     *                      0 - basic damage (no status damage)
     *                      1 - fire damage  (sets player aflame)
     * Note: fire status buildup reduces water status buildup, and vice-versa
     */
    public virtual void takeDamage(float damage, bool right, int type = 0)
    {
        health -= damage;//apply damage
        statusBuildup += type * damage;//apply status buildup
        Debug.Log("Status:" + statusBuildup + " damage:" + damage + " right:" + right + " type:" + type);

        if (statusBuildup >= fireResist)
        {
            //set agent aflame for a time (idea: time determined by overflow)
            burnTimer = burnDuration;
            damageTimer = damageInterval;
            statusBuildup = 0;
        }

        else if (statusBuildup <= waterResist)
        {
            //slow agent for a time (idea: time determined by overflow)
            moveController.TriggerWaterEffect(3);
            statusBuildup = 0;
        }

        moveController.knockback(right, damage > 0);
    }

    public virtual int getHealth() {
        return (int)health;
    }
}
