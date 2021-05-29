using System;
using UnityEngine.Events;

public interface IShooter
{
    Action OnKilledEnemy { get; set; }
    Action OnShoot { get; set; }

    void SubscribeToProjectile(Projectile projectile);
}