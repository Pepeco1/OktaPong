using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Health : MonoBehaviour
{

    [SerializeField] private int maxHealth = 100;
    private int currentHealth = 0;

    public UnityAction onDeath = null;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public bool TakeDamage(int amount)
    {
        if((currentHealth - amount) > 0)
        {
            ChangeHealth(-amount);
            return false;
        }
        else
        {
            ChangeHealth(-currentHealth);
            onDeath?.Invoke();
            return true;
        }


    }

    public void Heal(int amount)
    {
        if((currentHealth + amount) < maxHealth)
        {
            ChangeHealth(amount);
        }
        else
        {
            ChangeHealth(maxHealth - currentHealth);
        }
    }

    private void ChangeHealth(int amount)
    {
        currentHealth += amount;
    }


}
