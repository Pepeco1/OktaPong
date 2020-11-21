using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ship : MovableObjectMono, IDamageable
{
    public Health Health { get => health; set => health = value; }


    private InputProvider input = null;

    private Health health = null;

    [SerializeField] private ProjectilePool projectilePool = null;

    private List<Gun> gunList = null;


    private void Awake()
    {
        input = GetComponent<InputProvider>();
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

    private bool CanMove(Vector2 direction, float distance, float yOffset)
    {
        var positionToCastRay = new Vector2(transform.position.x, transform.position.y + yOffset);
        if(!Physics2D.Raycast(positionToCastRay, direction, distance))
        {
            return true;
        }
        return false;
    }

    protected override void Move()
    {
        if (input.VerticalInput != 0)
        {

            float yOffset = 0f;
            yOffset = input.VerticalInput < 0 ? -0.5f : 0.5f;

            var direction = Vector2.up * input.VerticalInput;
            var distance = MaxSpeed * Time.deltaTime;
            if (CanMove(direction, distance, yOffset))
            {
                transform.position = (Vector2) transform.position + direction * distance;
            }
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
