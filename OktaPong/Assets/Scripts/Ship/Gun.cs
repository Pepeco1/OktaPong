using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public bool CanShoot
    {
        get
        {
            return ShootingPermission && (nextShootTime < Time.time);
        }
    }
    public bool ShootingPermission { get => shootingPermission; set => shootingPermission = value; }
    public ProjectilePool ProjectilePool { set => projectilePool = value; }

    [SerializeField] private Transform gunTip = null;

    [SerializeField] private float shootDelay = 1f;
    private float nextShootTime = 0f;
    private bool shootingPermission = true;
    private ProjectilePool projectilePool = null;

    public void Shoot()
    {
        if (CanShoot)
        {

            var projectile = projectilePool.Get();
            projectile.ProjectilePool = projectilePool;
            projectile.transform.rotation = transform.rotation;
            projectile.transform.position = gunTip.position;
            projectile.gameObject.SetActive(true);
        
            nextShootTime = Time.time + shootDelay;
        
        }


    }


}
