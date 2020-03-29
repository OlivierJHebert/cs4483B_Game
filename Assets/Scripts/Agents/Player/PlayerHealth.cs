using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public GameObject playerDeathManager;
    public bool isBlocking = false;//flag for whether player is blocking or not
    public float blockCooldown = 1;//time between blocks
    private float blockTimer = 0;//time until next allowed block
    protected override void Start()
    {
        int stat_HP = PlayerPrefs.GetInt("HP");
        maxHealth = stat_HP * 4;

        if(stat_HP < 5)
        {
            waterResist -= stat_HP / 2;
            fireResist += stat_HP / 2;
        }

        else
        {
            waterResist = maxHealth / 2;
            fireResist = maxHealth / 2;
        }

        base.Start();
    }
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerPrefs.GetInt("HP") >= 3 && blockTimer <= 0)
        {
            isBlocking = true;
        }

        else if (blockTimer > 0)
        {
            blockTimer -= Time.deltaTime;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift) && isBlocking)
        {
            isBlocking = false;
        }

        if (isBlocking) Debug.Log("Blocking...");

        base.Update();
    }
    public override void Kill()
    {
        playerDeathManager.GetComponent<PlayerDeath>().KillPlayer();
    }

    public override void takeDamage(float damage, bool right, int type = 0)
    {
        if (isBlocking)
        {
            isBlocking = false;//block the hit
            blockTimer += blockCooldown;//disallow blocking while recharging
            gameObject.GetComponent<PlayerMove>().knockback(right, false);
        }

        else
        {
            base.takeDamage(damage, right, type);//take the damage
        }
        
    }
}
