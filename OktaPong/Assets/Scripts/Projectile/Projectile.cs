using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MovableObjectMono
{
    
    public ProjectilePool ProjectilePool { set => projectilePool = value; }
    public UnityAction OnCollide { get => onCollide; set => onCollide = value; }
    public UnityAction OnDealDamage { get => onDealDamage; set => onDealDamage = value; }
    public UnityAction OnKilledEnemy { get => onKill; set => onKill = value; }



    //Atributes
    [SerializeField] private int projectileDamage = 10;
    [SerializeField] private float programmedDeath = 1.5f;
    private int damageMultiplayer = 1;

    //Members
    private ProjectilePool projectilePool = null;

    //Events
    private UnityAction onCollide = null;
    private UnityAction onDealDamage = null;
    private UnityAction onKill = null;

    #region Unity functions

    private void OnEnable()
    {
        //StartCoroutine(ProgrammedDeath());
    }

    private void FixedUpdate()
    {
        CheckCollision();
        Move();
    }

    //protected void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Bounce(collision.GetContact(0).normal);

        // if (collision.collider.CompareTag("Player"))
        // {
        //     var enemy = collision.collider.GetComponent<IDamageable>();
        //     bool killedEnemy = enemy.TakeDamage(projectileDamage * damageMultiplayer);

        //     if (killedEnemy)
        //     {
        //         onKill?.Invoke();
        //     }
            
        //     onDealDamage?.Invoke();

        //     ClearEvents();
        //     projectilePool.ReturnToPool(this);
        // }

    #endregion

    #region private methods

    private IEnumerator ProgrammedDeath()
    {
        yield return new WaitForSeconds(programmedDeath);
        projectilePool.ReturnToPool(this);
    }

    private void Bounce(Vector2 collisionNormal)
    {
        var newDirection = Vector2.Reflect(transform.right, collisionNormal);
        transform.right = newDirection;
    }

    private void ClearEvents()
    {
        onCollide = null;
        onDealDamage = null;
        onKill = null;
    }

    #endregion

    #region override methods
    protected override void Move()
    {
        var newPosition = transform.right * MaxSpeed * Time.fixedDeltaTime;
        transform.position += newPosition;
    }

    #endregion

    private void CheckCollision()
    {
        var otherCollider = CheckOcurringCollision();

        if (otherCollider == null)
            return;

        var hits = new RaycastHit2D[1];
        var dir = (Vector3) otherCollider.ClosestPoint(transform.position) - transform.position;
        collider.Raycast(dir, hits, MaxSpeed * Time.fixedDeltaTime * 4f);

        TryCollisionWithDamageable(hits[0].collider);

        //Debug.Log(hits[0].normal);
        Bounce(hits[0].normal);
    }

    private void TryCollisionWithDamageable(Collider2D other)
    {
        var damageable = other.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        bool killed = damageable.TakeDamage(projectileDamage * damageMultiplayer);

        if (killed)
        {
            onKill?.Invoke();
        }

        onDealDamage?.Invoke();

        ClearEvents();
        projectilePool.ReturnToPool(this);
    }


}