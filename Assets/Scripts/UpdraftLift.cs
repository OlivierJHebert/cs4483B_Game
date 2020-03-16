using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdraftLift : MonoBehaviour
{
    public float lift = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();
        body.velocity += Vector2.up * lift;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        this.OnTriggerEnter2D(collision);
    }
}
