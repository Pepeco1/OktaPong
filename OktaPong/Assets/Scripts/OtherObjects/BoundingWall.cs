using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BoundingWall : MonoBehaviour, IDamageable
{
    public Health Health { get => health;}

    private Health health = null;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public void Heal(int amount)
    {
        Health.Heal(amount);
    }

    public bool TakeDamage(int amount)
    {
        return health.TakeDamage(amount);
    }
}
