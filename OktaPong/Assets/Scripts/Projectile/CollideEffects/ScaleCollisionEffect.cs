using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCollisionEffect : ProjectileCollisionEffect
{

    [Tooltip("Scale boost aplyed when the projectile collides")]
    [SerializeField] float scaleEffect = 0.5f;

    private float currentBoos = 0f;

    protected override void Projectile_OnCollide()
    {

        if (currentBoos >= 5f)
            return;

        myProjectile.transform.localScale += new Vector3(scaleEffect, scaleEffect, scaleEffect);
        currentBoos += scaleEffect;
    }
}
