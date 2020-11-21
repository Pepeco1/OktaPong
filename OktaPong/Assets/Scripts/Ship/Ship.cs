using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ship : MovableObjectMono, IDamageable
{
    public Health Health { get => health; set => health = value; }


    private InputProvider input = null;
    private CharacterController characterController = null;

    private Health health = null;

    [SerializeField] private ProjectilePool projectilePool = null;

    private List<Gun> gunList = null;


    private void Awake()
    {
        input = GetComponent<InputProvider>();
        characterController = GetComponent<CharacterController>();
        gunList = GetComponentsInChildren<Gun>().ToList();
        health = GetComponent<Health>();

        InjectProjectilePoolInGuns();
    }

    private void InjectProjectilePoolInGuns()
    {
        gunList.ForEach(gun => gun.ProjectilePool = projectilePool);
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Move();
        Rotate();
    }

    public void TakeDamage(int amount)
    {
        Health.TakeDamage(amount);

        //Spawn particles
    }

    public void Heal(int amount)
    {
        Health.Heal(amount);
        //Spawn particles
    }

    private void Shoot()
    {
        if(input.ShootInput == true)
        {
            ShootAllGuns();
        }
    }

    private void ShootAllGuns()
    {
        gunList.ForEach(gun => gun.Shoot());
    }

    protected override void Move()
    {
        if (input.VerticalInput != 0)
        {
            characterController.Move(new Vector3(0, input.VerticalInput * MaxSpeed * Time.deltaTime, 0));
        }
    }

    protected override void Rotate()
    {
        if (input.HorizontalInput != 0)
        {
            transform.Rotate(new Vector3(0, 0, -input.HorizontalInput * RotationVelocity * Time.deltaTime));
        }
    }

}
