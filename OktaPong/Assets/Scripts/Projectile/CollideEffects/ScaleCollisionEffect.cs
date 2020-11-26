using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCollisionEffect : ProjectileCollisionEffect
{

    [Tooltip("Scale boost aplyed when the projectile collides")]
    [SerializeField] float scaleEffect = 0.5f;

    protected override void Projectile_OnCollide()
    {
        myProjectile.transform.localScale += new Vector3(scaleEffect, scaleEffect, scaleEffect);
    }
}
