using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Ship : MovableObjectMono, IDamageable, IShooter, IInputControlled, IScoreTrigger
{

    public Health Health { get => health; set => health = value; }
    public bool Permission { get => input.Permission; set => input.Permission = value; }
    public InputProvider InputProvider { get => input; }
    public Filiation Filiation { get => filiation; }
    public override float RotationAmount => base.RotationAmount * -input.HorizontalInput;

    public Action OnShoot { get => onShoot; set => onShoot = value; }
    public Action OnKilledEnemy { get => onKilledEnemy; set => onKilledEnemy = value; }
    public Action<Ship> OnDeath { get => onDeath; set => onDeath = value; }
    public Action<Filiation> OnScoreTrigger { get; set; }

    //Atributes
    [SerializeField] private Filiation filiation = Filiation.Player1;
    [SerializeField] private ProjectilePool projectilePool = null;
    private List<Gun> gunList = null;
    private InputProvider input = null;
    private Health health = null;

    // Events
    private Action onShoot = null;
    private Action onKilledEnemy = null;
    private Action<Ship> onDeath = null;


    #region Unity Functions

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<InputProvider>();
        gunList = GetComponentsInChildren<Gun>().ToList();
        health = GetComponent<Health>();

        InjectDependenciesInGuns();
    }

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

    #region private methods

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

            Permission = false;
            //input.TriggerTurnChangeEvent();
        }

    }

    private void ShootAllGuns()
    {
        gunList.ForEach(gun => gun.Shoot());
    }

    private bool CanMove(Vector2 direction, float distance)
    {
        var positionToCastRay = new Vector2(transform.position.x, transform.position.y);

        ContactFilter2D contact = new ContactFilter2D();
        var layer = Physics2D.DefaultRaycastLayers & ~LayerMask.GetMask("Player");
        contact.layerMask = layer;

        var hits = new RaycastHit2D[3];
        if (collider.Raycast(direction, hits, distance) <= 0)
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

            float yOffset = collider.bounds.extents.y;

            var direction = Vector2.up * input.VerticalInput;
            var distance = MaxSpeed * Time.deltaTime;
            if (CanMove(direction, distance + yOffset))
            {
                transform.position = (Vector2) transform.position + direction * distance;
            }
        }
    }

    #endregion

    #region Events

    public void SubscribeToProjectile(Projectile projectile)
    {
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
