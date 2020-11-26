using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Ship : MovableObjectMono, IDamageable, IShooter, IInputControlled, IScoreTrigger
{
    public Health Health { get => health; set => health = value; }
    public bool Permission { get => input.Permission; }
    public InputProvider InputProvider { get => input; }
    public Filiation Filiation { get => filiation; }
    public UnityAction OnShoot { get => onShoot; set => onShoot = value; }
    public UnityAction OnHit { get => onHit; set => onHit = value; }

    public UnityAction OnKilledEnemy { get => onKilledEnemy; set => onKilledEnemy = value; }
    public UnityAction<Ship> OnDeath { get => onDeath; set => onDeath = value; }
    public UnityAction<Filiation> OnScoreTrigger { get; set; }

    //Atributes
    [SerializeField] private Filiation filiation = Filiation.Player1;

    //Members
    [SerializeField] private ProjectilePool projectilePool = null;
    private List<Gun> gunList = null;
    private InputProvider input = null;
    private Health health = null;

    // Events
    private UnityAction onShoot = null;
    private UnityAction onHit = null;
    private UnityAction onKilledEnemy = null;
    private UnityAction<Ship> onDeath = null;


    #region Unity Functions

    private void Awake()
    {
        input = GetComponent<InputProvider>();
        gunList = GetComponentsInChildren<Gun>().ToList();
        health = GetComponent<Health>();

        InjectDependenciesInGuns();
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

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnSubscribeToEvents();
    }

    #endregion

    #region public methods
    public bool TakeDamage(int amount)
    {
        return Health.TakeDamage(amount);

        //Spawn particles
    }

    public void Heal(int amount)
    {
        Health.Heal(amount);
        //Spawn particles
    }
    #endregion

    #region private functions
    private void InjectDependenciesInGuns()
    {
        gunList.ForEach(gun => InjectMembers(gun));

        void InjectMembers(Gun gun)
        {
            gun.ProjectilePool = projectilePool;
            gun.Ship = this;
        }
    }

    private void Shoot()
    {
        if (input.ShootInput == true)
        {
            ShootAllGuns();
            input.TriggerTurnChangeEvent();
        }

    }

    private void ShootAllGuns()
    {
        gunList.ForEach(gun => gun.Shoot());
    }

    private bool CanMove(Vector2 direction, float distance, float yOffset)
    {
        var positionToCastRay = new Vector2(transform.position.x, transform.position.y + yOffset);
        if (!Physics2D.Raycast(positionToCastRay, direction, distance))
        {
            return true;
        }
        return false;
    }

    #endregion

    #region override methods
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
    #endregion

    #region Events

    public void SubscribeToProjectile(Projectile projectile)
    {
        projectile.OnDealDamage += Projectile_OnDealDamage;
        projectile.OnKilledEnemy += Projectile_OnKillEnemy;
    }

    public void TriggerTurnChangeEvent()
    {
        input.TriggerTurnChangeEvent();
    }

    private void Health_OnDeath()
    {
        OnDeath?.Invoke(this);
    }

    private void Projectile_OnDealDamage()
    {
        OnHit?.Invoke();
    }

    private void Projectile_OnKillEnemy()
    {
        onKilledEnemy?.Invoke();
        OnScoreTrigger?.Invoke(filiation);
    }

    private void SubscribeToEvents()
    {
        health.onDeath += Health_OnDeath;
    }

    private void UnSubscribeToEvents()
    {
        health.onDeath -= Health_OnDeath;
    }

    #endregion
}
