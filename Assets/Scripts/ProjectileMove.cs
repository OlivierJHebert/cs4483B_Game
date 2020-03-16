using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : PlayerHazardCollider
{

    public float speed = 10f;

    private Rigidbody2D m_Body;

    protected override void Start()
    {
        base.Start();
        m_Body = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        m_Body.velocity = Vector2.up;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        Destroy(this);
    }
}
