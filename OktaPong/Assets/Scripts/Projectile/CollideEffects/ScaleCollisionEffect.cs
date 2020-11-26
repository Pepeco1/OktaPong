using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCollisionEffect : ProjectileCollisionEffect
{

    [Tooltip("Scale boost aplyed when the projectile collides")]
    [SerializeField] float scaleEffect = 1.1f;

    protected override void Projectile_OnCollide()
    {
        myProjectile.transform.localScale *= scaleEffect;
    }
}
