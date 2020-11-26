using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticleOnHit : ProjectileCollisionEffect
{

    [SerializeField] private ParticleSystem hitParticle = null;

    protected override void Projectile_OnCollide()
    {
        Instantiate(hitParticle).transform.position = myProjectile.transform.position;
    }
}
