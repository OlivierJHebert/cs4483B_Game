using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHazardCollider : MonoBehaviour
{
    public GameObject player;
    public float damage = 5;

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player && !player.GetComponent<PlayerMove>().invincible())
        {
            // Knockback direction is determined by player facing because the spikes don't move
            bool right = player.GetComponent<PlayerMove>().facingRight();
            player.GetComponent<Health>().takeDamage(damage, right);
        }
    }
}
