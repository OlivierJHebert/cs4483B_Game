using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //death manager reference
    public float maxHealth = 10;
    private float health;
    public float fireResist = 5;
    public float waterResist = -5;
    private float statusBuildup = 0;

    private IMove moveController;
    protected virtual void Start()
    {
        health = maxHealth;
        moveController = gameObject.GetComponent<IMove>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Kill();
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
    public void takeDamage(float damage, int type = 0)
    {
        health -= damage;//apply damage
        statusBuildup += type * damage;//apply status buildup

        if (statusBuildup >= fireResist)
        {
            //set agent aflame for a time (idea: time determined by overflow)
        }

        else if (statusBuildup <= waterResist)
        {
            //slow agent for a time (idea: time determined by overflow)
            moveController.TriggerWaterEffect(1);
        }
    }
}
