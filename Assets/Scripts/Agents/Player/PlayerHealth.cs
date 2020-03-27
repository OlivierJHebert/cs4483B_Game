using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public GameObject playerDeathManager;
    protected override void Start()
    {
        maxHealth += PlayerPrefs.GetInt("HP");
        base.Start();
    }
    public override void Kill()
    {
        playerDeathManager.GetComponent<PlayerDeath>().KillPlayer();
    }
}
