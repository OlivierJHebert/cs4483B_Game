using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public GameObject playerDeathManager;
    public bool isBlocking = false;//flag for whether player is blocking or not
    protected override void Start()
    {
        int stat_HP = PlayerPrefs.GetInt("HP");
        maxHealth += stat_HP;

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
        if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerPrefs.GetInt("HP") >= 3)
        {
            isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && isBlocking)
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

    public override void takeDamage(float damage, int type = 0)
    {
        if (isBlocking)
        {
            isBlocking = false;//block the hit
        }

        else
        {
            base.takeDamage(damage, type);//take the damage
        }
        
    }
}
