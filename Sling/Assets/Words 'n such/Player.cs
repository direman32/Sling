using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    private int currentHealth;
    public int Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void HealthRegen(int healFactor)
    {
        Health = Health + healFactor;
    }
}
