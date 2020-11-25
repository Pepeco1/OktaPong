using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MovableObjectMono
{
    
    public ProjectilePool ProjectilePool { set => projectilePool = value; }
    public Ship Ship { set => myShip = value; }


    //Events
    public UnityAction onCollide = null;

    //Atributes
    [SerializeField] private int projectileDamage = 10;
    private int damageMultiplayer = 1;

    //Members
    private ProjectilePool projectilePool = null;
    private Ship myShip = null;


    #region Unity functions

    private void Start()
    {
        StartCoroutine(ProgrammedDeath());
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision.GetContact(0).normal);

        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<IDamageable>().TakeDamage(projectileDamage * damageMultiplayer);
            myShip.TriggerOnHitEvent();
        }

        onCollide?.Invoke();
    }
    #endregion

    private IEnumerator ProgrammedDeath()
    {
        yield return new WaitForSeconds(2f);
        myShip.TriggerTurnChangeEvent();
        projectilePool.ReturnToPool(this);
    }

    protected override void Move()
    {
        var newPosition = transform.right * MaxSpeed * Time.deltaTime;
        transform.position += newPosition;
    }

    private void Bounce(Vector2 collisionNormal)
    {
        var newDirection = Vector2.Reflect(transform.right, collisionNormal);
        transform.right = newDirection;
    }

}