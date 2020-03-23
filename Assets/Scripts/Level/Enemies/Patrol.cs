using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : PlayerHazardCollider
{
    public float patrolSpeed;
    public float distance;

    [SerializeField] private LayerMask platformsLayerMask;
    public Transform groundDetection;

    private bool m_MovingRight = true;

    protected override void Update()
    {
        base.Update();
        transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, platformsLayerMask);

        if(groundInfo.collider == false)
        {
            if(m_MovingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                m_MovingRight = false;
            }

            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                m_MovingRight = true;
            }
        }
    }
}
