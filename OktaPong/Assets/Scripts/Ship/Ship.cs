using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Ship : MovableObjectMono, IDamageable
{
    public Health Health { get => health; set => health = value; }
    public bool Permission { get => input.Permission;}


    //Members
    [SerializeField] private ProjectilePool projectilePool = null;
    private List<Gun> gunList = null;
    private InputProvider input = null;
    private Health health = null;

    // Events
    public UnityAction onShoot = null;


    private void Awake()
    {
        input = GetComponent<InputProvider>();
        gunList = GetComponentsInChildren<Gun>().ToList();
        health = GetComponent<Health>();

        InjectDependenciesInGuns();
    }

    private void InjectDependenciesInGuns()
    {
        gunList.ForEach(gun => InjectMembers(gun));

        void InjectMembers(Gun gun)
        {
            gun.ProjectilePool = projectilePool;
            gun.Ship = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Permission)
            return;

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

    void ShootAllGuns()
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

    public void TriggerTurnChangeEvent()
    {
        input.TriggerTurnChangeEvent();
    }

}
