using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    protected int currentHealth;
    public manager manager;


    public int Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    private void Awake()
    {
        Health = maxHealth;
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<manager>();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Dead();
        }
    }

    public int getHealth()
    {
       return Health;
    }

    public void HealthRegen(int healFactor)
    {
        Health = Health + healFactor;
    }

    private void Dead()
    {
        manager.gameEnded();
        Destroy(this.gameObject);
    }
}
