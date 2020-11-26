using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MovableObjectMono
{
    
    public int Damage { get => projectileDamage;}
    public float DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
    public float Bounciness { get => bounciness; set => bounciness = value; }

    public ProjectilePool ProjectilePool { set => projectilePool = value; }
    public UnityAction OnCollide { get => onCollide; set => onCollide = value; }
    public UnityAction OnKilledEnemy { get => onKill; set => onKill = value; }


    //Atributes
    [SerializeField] private int projectileDamage = 10;
    [SerializeField] private float programmedDeath = 1.5f;
    [SerializeField] private float bounciness = 1f;
    private float damageMultiplier = 1f;

    //Members
    private ProjectilePool projectilePool = null;

    //Events
    private UnityAction onCollide = null;
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
        MaxSpeed = MaxSpeed * bounciness;
    }

    private void ResetProjectile()
    {
        onCollide = null;
        onKill = null;

        transform.localScale = new Vector3(1, 1, 1);

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

        OnCollide?.Invoke();

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

        bool killed = damageable.TakeDamage(Mathf.FloorToInt(projectileDamage * damageMultiplier));

        if (killed)
        {
            onKill?.Invoke();
        }

        ResetProjectile();
        projectilePool.ReturnToPool(this);
    }


}