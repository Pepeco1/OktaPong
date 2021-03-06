﻿using UnityEngine;

[RequireComponent(typeof(Projectile))]
public abstract class ProjectileCollisionEffect : MonoBehaviour
{

    protected Projectile myProjectile = null;

    private void Awake()
    {
        myProjectile = GetComponent<Projectile>();
    }

    private void OnEnable()
    {
        myProjectile.OnCollide += Projectile_OnCollide;
    }

    private void OnDisable()
    {
        myProjectile.OnCollide -= Projectile_OnCollide;
    }

    protected abstract void Projectile_OnCollide();
}