using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Form
{
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float gravScale;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private PhysicsMaterial2D physicsMat;

    public Sprite Sprite { get => playerSprite; }
    public float WalkSpeed { get => walkSpeed; }
    public float JumpStrength { get => jumpStrength; }
    public float GravityScale { get => gravScale; }
    public float FallMultiplier { get => fallMultiplier; }
    public PhysicsMaterial2D Material { get => physicsMat; }

    public Form(Sprite pSprite, float wSpeed, float jStrength, float gScale, float fMultiplier, PhysicsMaterial2D pMaterial)
    {
        playerSprite = pSprite;
        walkSpeed = wSpeed;
        jumpStrength = jStrength;
        gravScale = gScale;
        fallMultiplier = fMultiplier;
        physicsMat = pMaterial;
    }

}
