using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MovableObjectMono
{
    
    public int Damage { get => projectileDamage; set => projectileDamage = value; }
    public float DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
    public float Bounciness { get => bounciness; set => bounciness = value; }

    public ProjectilePool ProjectilePool { get => projectilePool; set => projectilePool = value; }
    public Action OnCollide { get => onCollide; set => onCollide = value; }
    public Action OnKilledEnemy { get => onKill; set => onKill = value; }


    //Atributes
    [SerializeField] private int projectileDamage = 10;
    [SerializeField] private float programmedDeath = 4f;
    [SerializeField] private float bounciness = 1f;
    private float damageMultiplier = 1f;

    //Members
    private ProjectilePool projectilePool = null;

    //Events
    private Action onCollide = null;
    private Action onKill = null;

    #region Unity functions

    private void OnEnable()
    {
        StartCoroutine(ProgrammedDeath());
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {

        OnCollide?.Invoke();
        Bounce(collision.GetContact(0).normal);
        TryCollisionWithDamageable(collision.collider);

    }

    #endregion

    #region private methods

    private IEnumerator ProgrammedDeath()
    {
        yield return new WaitForSeconds(programmedDeath);
        ResetAndReturnToPool();
    }

    private void Bounce(Vector2 collisionNormal)
    {
        var newDirection = Vector2.Reflect(transform.right, collisionNormal);
        transform.right = newDirection;
        MaxSpeed = MaxSpeed * bounciness;
    }

    private void ResetProjectile()
    {
        onCollide = null;
        onKill = null;

        transform.localScale = new Vector3(1, 1, 1);

    }

    private void TryCollisionWithDamageable(Collider2D other)
    {
        var damageable = other?.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        bool killed = damageable.TakeDamage(Mathf.FloorToInt(projectileDamage * damageMultiplier));

        if (killed)
        {
            onKill?.Invoke();
        }

        ResetAndReturnToPool();
    }

    private void ResetAndReturnToPool()
    {
        TurnManager.Instance.NextTurn();
        ResetProjectile();
        projectilePool.ReturnToPool(this);
    }

    #endregion

    #region override methods
    protected override void Move()
    {
        var newPosition = transform.right * MaxSpeed * Time.fixedDeltaTime;
        transform.position += newPosition;
    }

    #endregion

    //private void CheckCollision()
    //{
    //    var otherCollider = CheckOcurringCollision();

    //    if (otherCollider == null)
    //        return;

    //    OnCollide?.Invoke();

    //    var hits = new RaycastHit2D[1];
    //    var dir = (Vector3) otherCollider.ClosestPoint(transform.position) - transform.position;
    //    collider.Raycast(dir, hits, MaxSpeed * Time.fixedDeltaTime * 4f);

    //    TryCollisionWithDamageable(hits[0].collider);

    //    //Debug.Log(hits[0].normal);
    //    Bounce(hits[0].normal);
    //}




}