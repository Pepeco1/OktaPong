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
    public Ship Ship { set => myShip = value;}

    //Atributes
    [SerializeField] private float shootDelay = 1f;
    private float nextShootTime = 0f;
    private bool shootingPermission = true;

    //Members
    [SerializeField] private Transform gunTip = null;
    private ProjectilePool projectilePool = null;
    private Ship myShip = null;

    public void Shoot()
    {
        if (CanShoot)
        {

            var projectile = projectilePool.Get();

            projectile.ProjectilePool = projectilePool;
            projectile.Ship = myShip;

            projectile.transform.rotation = transform.rotation;
            projectile.transform.position = gunTip.position;
            projectile.gameObject.SetActive(true);
        
            nextShootTime = Time.time + shootDelay;
        
        }


    }


}
