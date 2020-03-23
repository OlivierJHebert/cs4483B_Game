using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float health = 10;

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);//kills the entity, replace with death method for animation later
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage!");
    }
}
